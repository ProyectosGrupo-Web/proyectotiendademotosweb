using Microsoft.AspNetCore.Mvc;
using webmotos.Filters;
using webmotos.Models;
using webmotos.Models.Security;
using webmotos.Services;

namespace webmotos.Controllers
{
    public class LoginController : Controller
    {

        private readonly UsuarioService _usuarioService;

        public LoginController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        
        [NoLogin]
        public ActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Acceder(string Usuario, string Password)
        {
            //usuario.testDB();
            // Llama al método Acceder y pasa HttpContext
            var rm = await _usuarioService.Acceder(HttpContext, Usuario, Password);

            if (rm.response) // Verifica si la respuesta fue exitosa
            {
                return RedirectToAction("Index", "Admin");
            }

            TempData["ErrorMessage"] = "Credenciales incorrectas. Por favor, intente nuevamente.";
            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> Logout()
        {
            await SessionHelper.DestroyUserSession(HttpContext);
            return Redirect("~/Login");
        }

    }
}
