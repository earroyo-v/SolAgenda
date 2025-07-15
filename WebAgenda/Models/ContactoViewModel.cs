using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAgenda.Models
{
    public class ContactoViewModel
    {
        public int IdContacto { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string FechaNacimiento { get; set; }
        public string Foto { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }
        public List<String> RedSocial { get; set; } = new List<string>();
        public List<String> Perfil { get; set; } = new List<string>();
        public int Edad
        {
            get
            {
                int edad = DateTime.Now.Year - Convert.ToDateTime(FechaNacimiento).Year;
                if (Convert.ToDateTime(FechaNacimiento) > DateTime.Now.AddYears(-edad))
                {
                    edad--;
                }
                return edad;
            }
            set
            {
                Edad = value;
            }
        }
    }
}