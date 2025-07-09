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
            datos.Update(user);
        }
        public void Borrar(int id)
        {
            datos.Delete(id);
        }
    }
}
