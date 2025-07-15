using Datos.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_Contacto
    {
        public void Create(Contacto contact)
        {
            try
            {
                using (var db = new GENERACION33Entities())
                {
                    db.Contacto.Add(contact);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Contacto> ReadAll(int id)
        {
            List<Contacto> contacts = new List<Contacto>();
            try
            {
                using (var db = new GENERACION33Entities())
                {
                    contacts = db.Contacto.Include("Usuario").Where(user => user.IdUsuario == id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return contacts;
        }
        public Contacto ReadId(int id)
        {
            Contacto contact = new Contacto();
            try
            {
                using (var db = new GENERACION33Entities())
                {
                    contact = db.Contacto.Find(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return contact;
        }
        public void Update(Contacto contact)
        {
            try
            {
                using (var db = new GENERACION33Entities())
                {
                    db.Contacto.AddOrUpdate(contact);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Delete(int id)
        {
            try
            {
                using (var db = new GENERACION33Entities())
                {
                    Contacto contact = db.Contacto.Find(id);
                    db.Contacto.Remove(contact);
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
