using Datos.Model;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebAgenda.Models;

namespace WebAgenda.Controllers
{
    public class HomeController : Controller
    {
        N_Usuario neg = new N_Usuario();
        // GET: Home
        public ActionResult Index()
        {
            return View("LoginView");
        }
        public ActionResult LogIn(string User, string Password)
        {
            try
            {
                Usuario data = neg.ValidarIngreso(User, Password);
                UsuarioSessionViewModel user = new UsuarioSessionViewModel()
                {
                    IdUsuario = data.IdUsuario,
                    Nombre = data.Nombre,
                    ApellidoPaterno = data.ApellidoPaterno,
                    ApellidoMaterno = data.ApellidoMaterno,
                    FechaNacimiento = data.FechaNacimiento,
                    Email = data.Email,
                    NickName = data.NickName,
                    Foto = data.Foto,
                    UrlPerfil = data.UrlPerfil
                };
                Session["Usuario"] = user;
                return RedirectToAction("Index", "Agenda");
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
        public ActionResult UserCreateView()
        {
            return View();
        }
        public ActionResult AgregarUsuario(Usuario user, HttpPostedFileBase ArchivoImagen)
        {
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
                    ArchivoImagen.SaveAs(Path.Combine(path, ArchivoImagen.FileName));
                    user.Foto = ArchivoImagen.FileName;
                }
                else
                {
                    user.Foto = "";
                }
                neg.Agregar(user);
                TempData["m"] = "El usuario se agrego correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return RedirectToAction("UserCreateView");
            }
        }
        public ActionResult LogOut()
        {
            try
            {
                Session["Usuario"] = null;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}