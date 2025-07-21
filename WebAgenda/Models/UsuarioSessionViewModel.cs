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
        public string FechaString
        {
            get
            {
                return FechaNacimiento.ToString("yyyy-MM-dd");
            }
            set
            {
                FechaString = value;
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