using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Model
{
    [MetadataType(typeof(atributos))]
    public partial class Usuario
    {
        private string fecha;

        public string FechaFormato
        {
            get
            {
                return fecha = FechaNacimiento.ToShortDateString();
            }
            set
            {
                fecha = value;
            }
        }

    }
    class atributos
    {        
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Apellido Paterno")]
        [Required(ErrorMessage = "Apellido Paterno")]
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        [Required(ErrorMessage = "Fecha Nacimiento")]
        public System.DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "NickName")]
        public string NickName { get; set; }
        [Required(ErrorMessage = "Password")]
        public string Password { get; set; }
        public string Foto { get; set; }
        public string UrlPerfil { get; set; }
    }
}
