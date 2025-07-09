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
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string Foto { get; set; }
        public string UrlPerfil { get; set; }
    }
}
