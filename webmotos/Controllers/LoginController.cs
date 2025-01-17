using Microsoft.AspNetCore.Mvc;
using webmotos.Filters;
using webmotos.Models;
using webmotos.Models.Security;

namespace moduloNotificaciones.Controllers
{
    public class LoginController : Controller
    {
        private Usuario usuario = new Usuario();
        [NoLogin]
        public ActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Acceder(string Usuario, string Password)
        {
            //usuario.testDB();
            // Llama al método Acceder y pasa HttpContext
            var rm = await usuario.Acceder(HttpContext, Usuario, Password);

            if (rm.response) // Verifica si la respuesta fue exitosa
            {
                return RedirectToAction("Index", "Home");
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
