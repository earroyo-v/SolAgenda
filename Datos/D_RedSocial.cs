using Datos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_RedSocial
    {
        public List<RedSocial> Read()
        {
            List<RedSocial> rs = new List<RedSocial>();
            try
            {
                using (var db = new GENERACION33Entities())
                {
                    rs = db.RedSocial.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rs;
        }
    }
}
