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
        public List<spPerfilSocial_Result> GetLink(int idUser, int idContact)
        {
            List<spPerfilSocial_Result> list = new List<spPerfilSocial_Result>();
            try
            {
                using (var db = new GENERACION33Entities())
                {
                    list = db.spPerfilSocial(idUser,idContact).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
    }
}