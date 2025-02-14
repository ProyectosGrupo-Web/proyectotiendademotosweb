using Microsoft.AspNetCore.Mvc;

namespace webmotos.Controllers
{
    public class PageController : Controller
    {
        public IActionResult Terminos()
        {
            return View();
        }

        public IActionResult Privacidad()
        {
            return View();
        }

        public IActionResult Cookies()
        {
            return View();
        }
    }
}
