using Datos;
using Datos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_RedSocial
    {
        D_RedSocial datoBD = new D_RedSocial();
        public List<RedSocial> Obtener()
        {
            return datoBD.Read();
        }
    }
}
