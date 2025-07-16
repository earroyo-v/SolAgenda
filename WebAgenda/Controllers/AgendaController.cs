using Datos;
using Datos.Model;
using Negocio;
using System;
using System.Collections.Generic;
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
                ViewBag.RedSocial = new SelectList(new N_RedSocial().Obtener(), "IdRedSocial", "Nombre");
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
            }
            return View();
        }
        public ActionResult Agregar(ContactoViewModel view)
        {
            var user = (UsuarioSessionViewModel)Session["Usuario"];
            try
            {
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
                if (view.Perfil[0] != "")
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
                TempData["e"] = ex.Message;
                return RedirectToAction("AgregarView");
            }
        }
        public ActionResult EditarView()
        {
            return View();
        }
        public ActionResult Editar(int id)
        {
            return View();
        }
        public ActionResult EliminarView(int id)
        {
            try
            {
                var contacto = new ContactoViewModel()
                {
                    IdContacto = id
                };
                return View(contacto);
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
    }
}