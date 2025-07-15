using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Model;
using Datos;

namespace Negocio
{
    public class N_PerfilSocial
    {
        public List<spPerfilSocial_Result> Perfil(int idUser, int idContact)
        {
            return new D_PerfilSocial().GetLink(idUser,idContact);
        }
    }
}
