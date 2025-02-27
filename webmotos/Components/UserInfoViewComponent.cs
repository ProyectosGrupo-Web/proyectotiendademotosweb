using Microsoft.AspNetCore.Mvc;
using webmotos.Models.Security;
using webmotos.Services;

namespace webmotos.Components
{
    public class UserInfoViewComponent : ViewComponent
    {
        private readonly UsuarioService _usuarioService;
        private readonly CookieHelper _cookieHelper;

        public UserInfoViewComponent(UsuarioService usuarioService, CookieHelper cookieHelper)
        {
            _usuarioService = usuarioService;
            _cookieHelper = cookieHelper;
        }

        public IViewComponentResult Invoke()
        {
            var userId = _cookieHelper.GetIdFromCookie(HttpContext); // Obtiene el ID desde la cookie

            if (userId == null)
                return View(null); // Si no está autenticado, no muestra nada.

            var usuario = _usuarioService.Obtener(userId.Value); // Llama a Obtener() sin async

            return View(usuario); // Retorna la vista con el usuario
        }

    }
}
