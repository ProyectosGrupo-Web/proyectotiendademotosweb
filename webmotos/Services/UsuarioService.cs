using Microsoft.EntityFrameworkCore;
using webmotos.Models;
using webmotos.Models.Security;

namespace webmotos.Services
{
    public class UsuarioService
    {
        private readonly WebmotosContext _context;

        // Inyección del DbContext a través del constructor
        public UsuarioService(WebmotosContext context)
        {
            _context = context;
        }

        //Metodos
        public Usuario? Obtener(int id)
        {
            try
            {
                return _context.Usuarios.SingleOrDefault(x => x.IdUsuario == id);
            }
            catch (Exception ex)
            {
                // Manejar error (log, throw, etc.)
                throw new Exception("Error al obtener usuario", ex);
            }
        }
        public async Task<ResponseModel> Acceder(HttpContext httpContext, string email, string password)
        {
            var rm = new ResponseModel();
            try
            {
                string passwordEncriptada = HashHelper.ComputeMD5(password);
                var usuario = await _context.Usuarios
                                            .SingleOrDefaultAsync(x => x.Email == email && x.Password == passwordEncriptada);

                if (usuario != null)
                {
                    Console.WriteLine("SE VALIDÓ LA SESIÓN");
                    await SessionHelper.AddUserToSession(httpContext, usuario.IdUsuario.ToString());
                    rm.SetResponse(true);
                }
                else
                {
                    rm.SetResponse(false, "Usuario y/o Password incorrectos...");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, "Ocurrió un error al intentar acceder. " + ex.Message);
            }

            return rm;
        }

        // METODOS DE SESIONES
        // Métodos
        
        //public Usuario Obtener(int id)
        //{
        //    var usuario = new Usuario();
        //    try
        //    {
        //        using (var db = new WebmotosContext())
        //        {
        //            usuario = db.Usuarios
        //                        .Where(x => x.IdUsuario == id)
        //                        .SingleOrDefault();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return usuario;
        //}

        //public async Task<ResponseModel> Acceder(HttpContext httpContext, string Usuario, string Password)
        //{
        //    var rm = new ResponseModel();

        //    try
        //    {
        //        using (var db = new WebmotosContext())
        //        {
        //            // Encriptar el password usando MD5
        //            Password = HashHelper.ComputeMD5(Password);
        //            var usuario = db.Usuarios
        //                            .Where(x => x.Email == Usuario && x.Password == Password)
        //                            .SingleOrDefault();


        //            if (usuario != null)
        //            {
        //                // Agregar al usuario a la sesión
        //                Console.WriteLine("SE VALIDO LA SESION");
        //                await SessionHelper.AddUserToSession(httpContext, usuario.IdUsuario.ToString());
        //                rm.SetResponse(true);
        //            }
        //            else
        //            {
        //                rm.SetResponse(false, "Usuario y/o Password incorrectos...");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Registrar o manejar la excepción
        //        rm.SetResponse(false, "Ocurrió un error al intentar acceder." + ex);
        //    }
        //    Console.WriteLine(rm);
        //    return rm;
        //}
    }
}
