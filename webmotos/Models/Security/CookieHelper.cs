using System;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
namespace webmotos.Models.Security;

public class CookieHelper
{
    
    public int? GetIdFromCookie(HttpContext httpContext)
    {
        // Intenta obtener el Claim del userId
        var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            return userId; // Devuelve el ID del usuario
        }

        return null; // Si no encuentra el ID, retorna null
    }





}
