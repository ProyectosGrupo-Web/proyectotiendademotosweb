using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webmotos.Models;
using webmotos.Services;

namespace webmotos.Controllers
{
    public class MotoController : Controller
    {
        //private Marca objMarca = new Marca();
        //private Tipo objTipo = new Tipo();
        //private Modelo objModelo = new Modelo();

        //private Moto objMotos = new Moto();

        // Instancia del servicio
        private readonly TipoService _tipoService;
        private readonly MotoService _motoService;
        // Constructor del controlador donde solo se inyecta Moto
        public MotoController(MotoService motoService, TipoService tipoService)
        {
            _motoService = motoService;
            _tipoService = tipoService;
        }

        [HttpGet("Moto")]
        public ActionResult Index()
        {
            var tipos = _tipoService.ObtenerTipos();
            ViewBag.Tipos = tipos;

            if (tipos != null)
            {
                foreach (var tipo in tipos)
                {
                    Console.WriteLine($"Tipo: {tipo.Tipo1}");
                }
            }
            else
            {
                Console.WriteLine("ObtenerTipos() devolvió NULL");
            }

            //var motos = _motoService.listarMotosConFotos();
            var motos = _motoService.FiltrarMotosPorTipo("");

            // Debug: Verifica si las fotos están cargadas
            foreach (var moto in motos)
            {
                Console.WriteLine($"Moto: {moto.IdMoto}, Fotos: {string.Join(", ", moto.Fotos.Select(f => f.UrlFoto))}");
            }
            return View("Index", motos);
        }

        [HttpGet("Moto/{tipo}")]
        public ActionResult FiltrarPorTipo(string tipo)
        {
            var tipos = _tipoService.ObtenerTipos();
            ViewBag.Tipos = _tipoService.ObtenerTipos();

            if (string.IsNullOrEmpty(tipo))
            {
                return RedirectToAction("Index"); // Si no hay filtro, se muestran todas
            }

            var motosFiltradas = _motoService.FiltrarMotosPorTipo(tipo);

            return PartialView("Index", motosFiltradas); // Se usa una vista parcial
        }


        //// Acciones para Marcas
        //public ActionResult mostrarMarcas()
        //{
        //    return View(objMarca.listarMarcas());
        //}
        //// Acciones para Tipos
        //public ActionResult mostrarTipos()
        //{
        //    return View(objTipo.listarTipos());
        //}

        //// Acciones para Modelos
        //public ActionResult mostrarModelos()
        //{
        //    return View(objModelo.listarModelos());
        //}

        // Acciones para Motos

        //[HttpGet("Moto")]
        //public ActionResult mostrarMotos()
        //{
        //    var tipos = _tipoService.ObtenerTipos();
        //    ViewBag.Tipos = _tipoService.ObtenerTipos();

        //    if (tipos != null)
        //    {
        //        foreach (var tipo in tipos)
        //        {
        //            Console.WriteLine($"Tipo: {tipo.Tipo1}");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("ObtenerTipos() devolvió NULL");
        //    }
        //    //
        //    var motos = _motoService.listarMotosConFotos();

        //    // Debug: Verifica si las fotos están cargadas
        //    foreach (var moto in motos)
        //    {
        //        Console.WriteLine($"Moto: {moto.IdMoto}, Fotos: {string.Join(", ", moto.Fotos.Select(f => f.UrlFoto))}");
        //    }

        //    return View(motos);
        //}

        [HttpGet("Moto/Todas")]
        public IActionResult Todas()
        {
            var motos = _motoService.FiltrarMotosPorTipo("");
            return View("Todas", motos);
        }

        [HttpGet("Moto/Deportivas-Pisteras")]
        public IActionResult DeportivasPisteras()
        {
            var motos = _motoService.FiltrarMotosPorTipo("Deportivas-Pisteras");
            return View("DeportivasPisteras", motos);
        }

        [HttpGet("Moto/JchMotos")]
        public IActionResult JchMotos()
        {
            var motos = _motoService.FiltrarMotosPorTipo("Jch motos");
            return View("JchMotos", motos);
        }

        [HttpGet("Moto/BrunoMotos")]
        public IActionResult BrunoMotos()
        {
            var motos = _motoService.FiltrarMotosPorTipo("Bruno motos");
            return View("BrunoMotos", motos);
        }


    }
}
