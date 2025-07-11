using Datos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAgenda.Controllers
{
    public class AgendaController : Controller
    {
        // GET: Agenda
        public ActionResult Index()
        {
            List<Contacto> contactos = new List<Contacto>();
            return View("AgendaView", contactos);
        }
    }
}