using Datos;
using Datos.Model;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebAgenda.Models;

namespace WebAgenda.Controllers
{
    public class AgendaController : Controller
    {
        N_Contacto datos = new N_Contacto();
        N_PerfilSocial datosPerfil = new N_PerfilSocial();
        // GET: Agenda
        public ActionResult Index()
        {
            List<ContactoViewModel> list = new List<ContactoViewModel>();
            try
            {
                /////////(TIPO DE DATO) Session["x"] --> Session es un tipo de caja que gurada cualquier valor, 
                ///para utilizar lo que hay adentro hay que hacer un unboxing y definir el tipo de dato que tiene la session
                var us = (UsuarioSessionViewModel)Session["Usuario"];
                foreach (var item in datos.Obtener(us.IdUsuario))
                {
                    var contacto = new ContactoViewModel()
                    {
                        IdContacto = item.IdContacto,
                        Nombre = item.Nombre,
                        ApellidoPaterno = item.ApellidoPaterno,
                        ApellidoMaterno = item.ApellidoMaterno,
                        FechaNacimiento = item.FechaNacimiento.ToShortDateString(),
                        Foto = item.Foto,
                        Telefono = item.Telefono,
                        Email = item.Email,
                        IdUsuario = item.IdUsuario
                    };
                    foreach (var otheritem in datosPerfil.Perfil(item.IdUsuario, item.IdContacto))
                    {
                        contacto.RedSocial.Add(otheritem.RedSocial);
                        contacto.Perfil.Add(otheritem.UrlPerfil);
                    }
                    list.Add(contacto);
                    TempData["t"] = list.Count;
                }
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
            }
            return View("AgendaView", list);
        }
        public ActionResult AgregarView()
        {
            try
            {
                ViewBag.RedSocialOpciones = new SelectList(new N_RedSocial().Obtener(), "IdRedSocial", "Nombre");
                return View();
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
        public ActionResult Agregar(ContactoViewModel view, HttpPostedFileBase ArchivoImagen)
        {
            var user = (UsuarioSessionViewModel)Session["Usuario"];
            try
            {
                if (ArchivoImagen != null)
                {
                    if (ArchivoImagen.ContentType != "image/png" && ArchivoImagen.ContentType != "image/jpeg") throw new Exception("La imagen debe ser .png o jpg");
                    string path = Server.MapPath("~/Imagenes");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    ArchivoImagen.SaveAs(Path.Combine(Server.MapPath("~/Imagenes"), ArchivoImagen.FileName));
                    view.Foto = ArchivoImagen.FileName;
                }
                else
                {
                    view.Foto = "";
                }

                //Convertir viewmodel a model -> se agrga id user
                var contacto = new Contacto()
                {
                    Nombre = view.Nombre,
                    ApellidoPaterno = view.ApellidoPaterno,
                    ApellidoMaterno = view.ApellidoMaterno,
                    FechaNacimiento = Convert.ToDateTime(view.FechaNacimiento),
                    Foto = view.Foto,
                    Telefono = view.Telefono,
                    Email = view.Email,
                    IdUsuario = user.IdUsuario
                };
                //agregar D_contactos
                datos.Agregar(contacto);// -> se podria usar trigger cuando se haga el sps de agregar justo despues se agregue el contacto
                //es el ultimo contacto relacionado al iduser 
                int IdContacto = datos.Obtener(user.IdUsuario).LastOrDefault().IdContacto;
                //si hay informacion en el perfil (no en red social) se agrega perfil(tabala relacionada) -> idcontacto, idredsocial                
                if (view.Perfil.Any())
                {
                    for (int i = 0; i < view.Perfil.Count; i++)
                    {
                        datosPerfil.Agregar(IdContacto, Convert.ToInt32(view.RedSocial[i]), view.Perfil[i]);
                    }
                }
                TempData["m"] = "Contacto Agregado";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                int id = view.IdContacto;
                TempData["e"] = ex.Message;
                return RedirectToAction("AgregarView", id);
            }
        }
        public ActionResult EditarView(int id)
        {
            try
            {
                Contacto item = datos.ObtenerId(id);
                var contacto = new ContactoViewModel()
                {
                    IdContacto = item.IdContacto,
                    Nombre = item.Nombre,
                    ApellidoPaterno = item.ApellidoPaterno,
                    ApellidoMaterno = item.ApellidoMaterno,
                    FechaNacimiento = item.FechaNacimiento.ToString("yyyy-MM-dd"),
                    Foto = item.Foto,
                    Telefono = item.Telefono,
                    Email = item.Email,
                    IdUsuario = item.IdUsuario

                };
                foreach (var otheritem in datosPerfil.Perfil(item.IdUsuario, item.IdContacto))
                {
                    if (otheritem.UrlPerfil != null)
                    {
                        contacto.IdPerfil.Add(otheritem.idPerfil.Value);
                        contacto.IdRedSocial.Add(otheritem.IdRedSocial.Value);
                        contacto.RedSocial.Add(otheritem.RedSocial);
                        contacto.Perfil.Add(otheritem.UrlPerfil);
                    }

                }
                ViewBag.RedSocialOpciones = new SelectList(new N_RedSocial().Obtener(), "IdRedSocial", "Nombre");

                var opcionesRedes = new List<SelectList>();
                for (int i = 0; i < contacto.Perfil.Count; i++)
                {
                    var selectList = new SelectList(new N_RedSocial().Obtener(), "IdRedSocial", "Nombre", contacto.IdRedSocial[i]);
                    opcionesRedes.Add(selectList);
                }
                ViewBag.IdRedSocial = opcionesRedes;
                return View(contacto);
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
        public ActionResult Editar(ContactoViewModel view, HttpPostedFileBase ArchivoImagen)
        {
            try
            {
                if (ArchivoImagen != null)
                {
                    if (ArchivoImagen.ContentType != "image/png" && ArchivoImagen.ContentType != "image/jpeg") throw new Exception("La imagen debe ser .png o jpg");
                    ArchivoImagen.SaveAs(Path.Combine(Server.MapPath("~/Imagenes"), ArchivoImagen.FileName));
                    view.Foto = ArchivoImagen.FileName;
                }
                else
                {
                    view.Foto = "";
                }
                //Convertir viewmodel a model
                var contacto = new Contacto()
                {
                    IdContacto = view.IdContacto,
                    Nombre = view.Nombre,
                    ApellidoPaterno = view.ApellidoPaterno,
                    ApellidoMaterno = view.ApellidoMaterno,
                    FechaNacimiento = Convert.ToDateTime(view.FechaNacimiento),
                    Foto = view.Foto,
                    Telefono = view.Telefono,
                    Email = view.Email,
                    IdUsuario = view.IdUsuario
                };
                //editar D_contactos
                datos.Editar(contacto);
                //si hay informacion en el perfil (no en red social) se agrega perfil(tabala relacionada) -> idcontacto, idredsocial                
                if (view.Perfil.Any())
                {
                    for (int i = 0; i < view.Perfil.Count; i++)
                    {
                        //Falta un metodo para borrar - en js si lo elimina pero aqui como lo veremos 
                        try
                        {
                            var perfil = new ContactoRedSocial()
                            {
                                IdContactoRedSocial = view.IdPerfil[i],
                                IdContacto = view.IdContacto,
                                IdRedSocial = view.IdRedSocial[i],
                                UrlPerfil = view.Perfil[i]
                            };
                            datosPerfil.Editar(perfil);
                        }
                        catch (Exception)
                        {
                            datosPerfil.Agregar(view.IdContacto, Convert.ToInt32(view.IdRedSocial[i]), view.Perfil[i]);
                        }
                    }
                }
                TempData["m"] = "Se edito el contacto correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                int id = view.IdContacto;
                TempData["e"] = ex.Message;
                return RedirectToAction("EditarView", new { id });
            }
        }
        public ActionResult EliminarView(int id)
        {
            try
            {
                var contacto = datos.ObtenerId(id);
                var view = new ContactoViewModel()
                {
                    IdContacto = contacto.IdContacto,
                    Nombre = contacto.Nombre,
                    ApellidoPaterno = contacto.ApellidoPaterno,
                    ApellidoMaterno = contacto.ApellidoMaterno,
                    FechaNacimiento = contacto.FechaNacimiento.ToShortDateString(),
                    Foto = contacto.Foto,
                    Telefono = contacto.Telefono,
                    Email = contacto.Email,
                    IdUsuario = contacto.IdUsuario
                };
                return View(view);
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
        public ActionResult Eliminar(int id)
        {
            try
            {
                datosPerfil.EliminarContacto(id);
                datos.Delete(id);
                TempData["m"] = "Se elimino el contacto correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return RedirectToAction("EliminarView", new { id });
            }
        }
        public ActionResult Buscar()
        {
            return View();
        }
        public ActionResult EliminarPerfil(int idPerfil, int idContacto)
        {
            int id = idContacto;
            try
            {
                datosPerfil.EliminarPerfil(idPerfil);
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
            }
            return RedirectToAction("EditarView", new { id });
        }
    }
}