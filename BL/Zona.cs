using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Zona
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaCineContext context = new DL.AacostaCineContext())
                {
                    var query = context.Zonas.FromSqlRaw("ZonaGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Zona zona = new ML.Zona();

                            zona.IdZona = obj.IdZona;
                            zona.Nombre = obj.Nombre;

                            result.Objects.Add(zona);
                        }
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}