using Datos.Model;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAgenda.Models;

namespace WebAgenda.Controllers
{
    public class AgendaController : Controller
    {
        N_Contacto datos = new N_Contacto();
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
                    foreach (var otheritem in new N_PerfilSocial().Perfil(item.IdUsuario, item.IdContacto))
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
            return View();
        }
        public ActionResult Agregar(int id)
        {
            return RedirectToAction("Index");
        }
        public ActionResult EditarView()
        {
            return View();
        }
        public ActionResult Editar(int id)
        {
            return View();
        }
        public ActionResult EliminarView()
        {
            return View();
        }
        public ActionResult Buscar()
        {
            return View();
        }
    }
}