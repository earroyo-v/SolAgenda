using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAgenda.Models
{
    public class UsuarioSessionViewModel
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Foto { get; set; }
        public string UrlPerfil { get; set; }
    }
}