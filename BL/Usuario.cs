using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        public static ML.Result UsuarioGetbyUserName(string UserName)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaCineContext context = new DL.AacostaCineContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetByUserName '{UserName}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = query.IdUsuario;
                        usuario.UserName = query.UserName;
                        usuario.Password = query.Password;

                        result.Object = usuario;
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}