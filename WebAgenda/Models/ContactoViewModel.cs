using Datos;
using Datos.Model;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace WebAgenda.Models
{
    public class ContactoViewModel
    {
        public int IdContacto { get; set; }
        [Display(Name = "Nombre(s)")]
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string FechaNacimiento { get; set; }
        public string Foto { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }
        public List<int> IdPerfil { get; set; } = new List<int>();
        public List<int> IdRedSocial { get; set; } = new List<int>();
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
        public int BirthDay
        {
            get
            {
                int cumple = 0;
                if (DateTime.Now.Day == Convert.ToDateTime(FechaNacimiento).Day && DateTime.Now.Month == Convert.ToDateTime(FechaNacimiento).Month)
                {
                    cumple = 1;
                }
                return cumple;
            }
            set
            {
                BirthDay = value;
            }
        }
    }
}