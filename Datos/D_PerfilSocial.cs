using Datos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_PerfilSocial
    {
        public void Create(int idContact, int idRedSocial, string url)
        {
            try
            {
                using (var db = new GENERACION33Entities())
                {
                    ContactoRedSocial contactoRedSocial = new ContactoRedSocial()
                    {
                        IdContacto = idContact,
                        IdRedSocial = idRedSocial,
                        UrlPerfil = url
                    };
                    db.ContactoRedSocial.Add(contactoRedSocial);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<spPerfilSocial_Result> GetLink(int idUser, int idContact)
        {
            List<spPerfilSocial_Result> list = new List<spPerfilSocial_Result>();
            try
            {
                using (var db = new GENERACION33Entities())
                {
                    list = db.spPerfilSocial(idUser, idContact).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public void DeleteContact(int id)
        {
            try
            {
                using (var db = new GENERACION33Entities())
                {
                    db.ContactoRedSocial.RemoveRange(db.ContactoRedSocial.Where(x => x.IdContacto == id));
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}