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
        D_PerfilSocial perfil = new D_PerfilSocial();
        public void Agregar(int idContacto, int idRs, string url)
        {
            perfil.Create(idContacto, idRs, url);
        }
        public List<spPerfilSocial_Result> Perfil(int idUser, int idContact)
        {
            return perfil.GetLink(idUser, idContact);
        }
        public void Editar(ContactoRedSocial data)
        {
            perfil.Edit(data);
        }
        public void EliminarContacto(int id)
        {
            perfil.DeleteContact(id);
        }
        public void EliminarPerfil(int id)
        {
            perfil.DeletePerfil(id);
        }
    }
}
