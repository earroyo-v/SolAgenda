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
    public class UserController : Controller
    {
        N_Usuario datos = new N_Usuario();
        // GET: User
        public ActionResult Index(int id)
        {
            try
            {
                var user = datos.ObtenerId(id);
                UsuarioSessionViewModel view = new UsuarioSessionViewModel()
                {
                    IdUsuario = user.IdUsuario,
                    Nombre = user.Nombre,
                    ApellidoPaterno = user.ApellidoPaterno,
                    ApellidoMaterno = user.ApellidoMaterno,
                    FechaNacimiento = user.FechaNacimiento,
                    Email = user.Email,
                    NickName = user.NickName,
                    Foto = user.Foto,
                    UrlPerfil = user.UrlPerfil
                };
                Session["Usuario"] = view;
                return View("InfoView", view);
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return RedirectToAction("Index", "Agenda");
            }
        }
        public ActionResult EditarView(int id)
        {
            try
            {
                var user = datos.ObtenerId(id);
                UsuarioSessionViewModel view = new UsuarioSessionViewModel()
                {
                    IdUsuario = user.IdUsuario,
                    Nombre = user.Nombre,
                    ApellidoPaterno = user.ApellidoPaterno,
                    ApellidoMaterno = user.ApellidoMaterno,
                    FechaNacimiento = user.FechaNacimiento,
                    Email = user.Email,
                    NickName = user.NickName,
                    Foto = user.Foto,
                    UrlPerfil = user.UrlPerfil
                };
                return View(view);
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return RedirectToAction("Index", "Agenda");
            }
        }
        public ActionResult Editar(UsuarioSessionViewModel view, HttpPostedFileBase ArchivoImagen)
        {
            int id = view.IdUsuario;
            try
            {
                if (ArchivoImagen != null)
                {
                    if (ArchivoImagen.ContentType != "image/png" && ArchivoImagen.ContentType != "image/jpeg") throw new Exception("La imagen debe ser .png o jpg");
                    ArchivoImagen.SaveAs(Path.Combine(Server.MapPath("~/Imagenes"), ArchivoImagen.FileName));
                    view.Foto = ArchivoImagen.FileName;
                }
                //if(ArchivoImagen == null && view.Foto == null)
                //{
                //    view.Foto = "";
                //}
                var usuario = new Usuario()
                {
                    IdUsuario = view.IdUsuario,
                    Nombre = view.Nombre,
                    ApellidoPaterno = view.ApellidoPaterno,
                    ApellidoMaterno = view.ApellidoMaterno,
                    FechaNacimiento = view.FechaNacimiento,
                    Email = view.Email,
                    NickName = view.NickName,
                    Foto = view.Foto,
                    UrlPerfil = view.UrlPerfil
                };
                datos.Editar(usuario);
                TempData["m"] = "Tus datos se editaron correctamente";
                return RedirectToAction("Index", new { id });
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return RedirectToAction("EditarView", new { id });
            }
        }
        public ActionResult CambiarPsswView()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return RedirectToAction("Index", "Agenda");
            }
        }
        public ActionResult CambiarPssw(string NewPssw, string CurrentPssw)
        {
            try
            {
                var session = (UsuarioSessionViewModel)Session["Usuario"];
                var user = datos.ValidarPassword(session.Email, CurrentPssw);
                user.Password = NewPssw;
                datos.Editar(user);
                TempData["m"] = "Contraseña actualizada correctamente";
                return RedirectToAction("Index", "Agenda");
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return View("CambiarPsswView");
            }

        }
        public ActionResult EliminarView()
        {
            try
            {
                var user = (UsuarioSessionViewModel)Session["Usuario"];
                return View((UsuarioSessionViewModel)Session["Usuario"]);
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return RedirectToAction("Index", "Agenda");
            }
        }
        public ActionResult Eliminar(int id)
        {
            try
            {
                N_Contacto datosContacto = new N_Contacto();
                foreach (var contact in datosContacto.Obtener(id))
                {
                    new N_PerfilSocial().EliminarContacto(contact.IdContacto);
                    datosContacto.Delete(contact.IdContacto);
                }
                datos.Borrar(id);
                TempData["m"] = "Usuario Elimnado";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return RedirectToAction("Eliminar");
            }
        }
    }
}