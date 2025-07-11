using Datos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;

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
                Usuario user = neg.ValidarIngreso(User, Password);
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