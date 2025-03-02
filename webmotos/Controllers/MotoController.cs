using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webmotos.Models;
using webmotos.Services;

namespace webmotos.Controllers
{
    public class MotoController : Controller
    {
        private readonly MotoService _motoService;
        private readonly TipoService _tipoService;

        // Constructor donde se inyectan los servicios
        public MotoController(MotoService motoService, TipoService tipoService)
        {
            _motoService = motoService;
            _tipoService = tipoService;
        }

        // Método para mostrar todas las motos
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

            var motos = _motoService.FiltrarMotosPorTipo("");

            // Debug: Verifica si las fotos están cargadas
            foreach (var moto in motos)
            {
                Console.WriteLine($"Moto: {moto.IdMoto}, Fotos: {string.Join(", ", moto.Fotos.Select(f => f.UrlFoto))}");
            }

            return View("Index", motos);
        }

        // Método para filtrar motos por tipo
        [HttpGet("Moto/{tipo?}")] // "?" hace que el parámetro sea opcional
        public IActionResult FiltrarPorTipo(string tipo)
        {
            var tipos = _tipoService.ObtenerTipos();
            ViewBag.Tipos = tipos;

            if (string.IsNullOrEmpty(tipo) || tipo == "Todas")
            {
                return RedirectToAction("Index"); // Si no hay filtro, muestra todas
            }

            var motosFiltradas = _motoService.FiltrarMotosPorTipo(tipo);
            return View("Index", motosFiltradas);
        }

        // Método para mostrar los detalles de una moto
        [HttpGet("Moto/Detalle/{id}")]
        public ActionResult Detalle(int id)
        {
            var moto = _motoService.detalleMoto(id);

            if (moto == null)
            {
                return NotFound("Moto no encontrada");
            }

            return View("DetalleMoto", moto);
        }
    }
}
