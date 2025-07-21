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
            if (contact.FechaNacimiento > DateTime.Now)
            {
                throw new Exception("La Fecha de nacimiento no puede ser mayor al dia de hoy");
            }
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
            if (contact.FechaNacimiento > DateTime.Now)
            {
                throw new Exception("La Fecha de nacimiento no puede ser mayor al dia de hoy");
            }
            contacto.Update(contact);
        }
        public void Delete(int id)
        {
            contacto.Delete(id);
        }
        public List<spBuscarContactos_Result> BuscarContacto(int idUser, string data)
        {
            return contacto.Search(idUser, data);
        }
    }
}
