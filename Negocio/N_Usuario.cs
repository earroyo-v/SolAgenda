using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Datos.Model;

namespace Negocio
{
    public class N_Usuario
    {
        D_Usuario datos = new D_Usuario();
        public void Agregar(Usuario user)
        {
            datos.Create(user);
        }
        public List<Usuario> Obtener()
        {
            List<Usuario> list = datos.Read();
            return list;
        }
        public Usuario ObtenerId(int id)
        {
            Usuario usuario = datos.ReadId(id);
            return usuario;
        }
        public void Editar(Usuario user)
        {
            if (user.Password == null)
            {
                user.Password = datos.ReadId(user.IdUsuario).Password;
            }
            datos.Update(user);
        }
        public void Borrar(int id)
        {
            datos.Delete(id);
        }
        public Usuario ValidarIngreso(string usr, string pssw)
        {
            Usuario usuario = datos.ReadUser(usr, pssw);
            if (usuario == null)
            {
                throw new System.Exception("Usuario o contrasena incorrectos");
            }
            return usuario;
        }
        public Usuario ValidarPassword(string usr, string pssw)
        {
            Usuario usuario = datos.ReadUser(usr, pssw);
            if (usuario == null)
            {
                throw new System.Exception("Contrasena incorrecta");
            }
            return usuario;
        }
    }
}
