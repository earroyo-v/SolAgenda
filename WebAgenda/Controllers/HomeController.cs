using Datos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
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
        public ActionResult AgregarUsuario(Usuario user)
        {
            try
            {
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