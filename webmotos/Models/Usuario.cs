using System;
using System.Collections.Generic;
using webmotos.Models.Security;

namespace webmotos.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreadoEn { get; set; }


    //Metodos
    // METODOS DE SESIONES
    public Usuario Obtener(int id)
    {
        var usuario = new Usuario();
        try
        {
            using (var db = new WebmotosContext())
            {
                usuario = db.Usuarios
                            .Where(x => x.IdUsuario == id)
                            .SingleOrDefault();
            }
        }
        catch (Exception)
        {
            throw;
        }
        return usuario;
    }

    public async Task<ResponseModel> Acceder(HttpContext httpContext, string Usuario, string Password)
    {
        var rm = new ResponseModel();

        try
        {
            using (var db = new WebmotosContext())
            {
                // Encriptar el password usando MD5
                Password = HashHelper.ComputeMD5(Password);
                var usuario = db.Usuarios
                                .Where(x => x.Email == Usuario && x.Password == Password)
                                .SingleOrDefault();


                if (usuario != null)
                {
                    // Agregar al usuario a la sesión
                    Console.WriteLine("SE VALIDO LA SESION");
                    await SessionHelper.AddUserToSession(httpContext, usuario.IdUsuario.ToString());
                    rm.SetResponse(true);
                }
                else
                {
                    rm.SetResponse(false, "Usuario y/o Password incorrectos...");
                }
            }
        }
        catch (Exception ex)
        {
            // Registrar o manejar la excepción
            rm.SetResponse(false, "Ocurrió un error al intentar acceder.");
        }
        Console.WriteLine(rm);
        return rm;
    }
}
