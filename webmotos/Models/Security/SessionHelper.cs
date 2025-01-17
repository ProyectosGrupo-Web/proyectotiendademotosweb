using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace webmotos.Models.Security;

public class SessionHelper
{
    //public static bool ExistUserInSession()
    //{
    //    return HttpContext.Current.User.Identity.IsAuthenticated;
    //}
    // VERSION PREVIAAA
    //public static bool ExistUserInSession(HttpContext httpContext)
    //{
    //    return httpContext.User.Identity?.IsAuthenticated ?? false;
    //}

    // NUEVA VERSION
    public static bool ExistUserInSession(HttpContext httpContext)
    {
        return httpContext.User.Identity.IsAuthenticated &&
               httpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null;
    }


    // END

    //public static void DestroyUserSession()
    //{
    //    FormsAuthentication.SignOut();
    //}
    public static async Task DestroyUserSession(HttpContext httpContext)
    {
        await httpContext.SignOutAsync();
    }


    //public static int GetUser()
    //{
    //    int user_id = 0;
    //    if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
    //    {
    //        FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
    //        if (ticket != null)
    //        {
    //            user_id = Convert.ToInt32(ticket.UserData);
    //        }
    //    }
    //    return user_id;
    //}
    public static string GetUser(HttpContext httpContext)
    {
        return httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }







    //public static void AddUserToSession(string id)
    //{
    //    bool persist = true;
    //    var cookie = FormsAuthentication.GetAuthCookie("usuario", persist);

    //    cookie.Name = FormsAuthentication.FormsCookieName;
    //    cookie.Expires = DateTime.Now.AddMonths(3);

    //    var ticket = FormsAuthentication.Decrypt(cookie.Value);
    //    var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate,
    //                                                  ticket.Expiration, ticket.IsPersistent, id);

    //    cookie.Value = FormsAuthentication.Encrypt(newTicket);
    //    HttpContext.Current.Response.Cookies.Add(cookie);
    //}

    public static async Task AddUserToSession(HttpContext httpContext, string userId)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId) // Solo el ID del usuario
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
        {
            IsPersistent = true,
            /* ExpiresUtc = DateTime.UtcNow.AddMonths(3) */ // Expiración de 3 meses
            /* ExpiresUtc = DateTime.UtcNow.AddMinutes(1) */
            ExpiresUtc = DateTime.UtcNow.AddHours(2)
        });
    }




    //public static string GetUserRole()
    //{
    //    if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
    //    {
    //        FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;

    //        if (ticket != null)
    //        {
    //            var userData = ticket.UserData.Split('|'); // Divide UserData (id|nombre_rol)
    //            if (userData.Length > 1) // Asegúrate de que tenga el ID y nombre del rol
    //            {
    //                return userData[1]; // Devuelve el nombre del rol
    //            }
    //        }
    //    }
    //    return null; // Si no encuentra el rol, devuelve null
    //}
    public static string GetUserRole(HttpContext httpContext)
    {
        return httpContext.User.FindFirst(ClaimTypes.Role)?.Value;
    }


}