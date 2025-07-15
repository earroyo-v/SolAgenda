using Datos;
using Datos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_Contacto
    {
        D_Contacto contacto = new D_Contacto();
        public void Agregar(Contacto contact)
        {
            contacto.Create(contact);
        }
        public List<Contacto> Obtener(int id)
        {
            //List<Contacto> list = contacto.ReadAll();
            return contacto.ReadAll(id);
        }
        public Contacto ObtenerId(int id)
        {
            //Contacto contact = contacto.ReadId(id);
            return contacto.ReadId(id);
        }
        public void Editar(Contacto contact)
        {
            contacto.Update(contact);
        }
        public void Delete(int id)
        {
            contacto.Delete(id);
        }
    }
}
