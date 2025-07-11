using Datos.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_Usuario
    {
        //private GENERACION33Entities db = new GENERACION33Entities();
        public void Create(Usuario user)
        {
            try
            {
                using (var db = new GENERACION33Entities())
                {
                    db.Usuario.Add(user);//No guarda inmediatamente en la base de datos, solo lo marca para guardarse. Aún puedes agregar más cosas antes de guardar.
                    db.SaveChanges();//Aquí es donde realmente se ejecuta el INSERT INTO en la base de datos y se guarda el nuevo usuario.
                    //db.Dispose();//Cierra la conexión y libera memoria.
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Usuario> Read()
        {
            List<Usuario> list = new List<Usuario>();
            try
            {
                using (GENERACION33Entities db = new GENERACION33Entities())
                {
                    list = db.Usuario.ToList();
                }//Dispose() automatico
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public Usuario ReadId(int id)
        {
            Usuario user = new Usuario();
            try
            {
                using (var db = new GENERACION33Entities())
                {
                    user = db.Usuario.Find(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }
        public void Update(Usuario user)
        {
            try
            {
                using (var db = new GENERACION33Entities())
                {
                    db.Usuario.AddOrUpdate(user);
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
                    Usuario user = db.Usuario.Find(id);
                    db.Usuario.Remove(user);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Usuario ReadUser(string usr, string pssw)
        {
            Usuario usuario = new Usuario();
            try
            {                
                using (var db = new GENERACION33Entities())
                {
                    usuario = db.Usuario.Where(x => x.Email == usr && x.Password == pssw).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return usuario;
        }
    }
}
