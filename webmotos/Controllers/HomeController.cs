using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using webmotos.Filters;
using webmotos.Models;
using webmotos.Services;

namespace webmotos.Controllers;
[Autenticado]
public class HomeController : Controller
{
    private readonly HomeService _homeService;


    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, HomeService homeService)
    {
        _logger = logger;
        _homeService = homeService;
    }

    public IActionResult Index()
    {
        var home = _homeService.listarElementosHome();
        return View(home);
    }

    

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
