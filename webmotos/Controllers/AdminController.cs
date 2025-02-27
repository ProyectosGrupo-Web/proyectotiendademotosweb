using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using webmotos.Filters;
using webmotos.Models;
using webmotos.Services;
using webmotos.Validators;

namespace webmotos.Controllers
{
    [Autenticado]
    public class AdminController : Controller
    {
        private readonly IValidator<Tipo> _validator;

        private readonly TipoService _tipoService;
        private readonly MarcasService _marcasService;
        private readonly ModelosService _modelosService;
        private readonly MotoService _motosService;
        private readonly FotoService _fotoService;
        public AdminController(IValidator<Tipo> validator, TipoService tipoService, MarcasService marcasService, ModelosService modelosService, MotoService motosService, FotoService fotoService)
        {
            _tipoService = tipoService;
            _marcasService = marcasService;
            _modelosService = modelosService;
            _motosService = motosService;
            _fotoService = fotoService;


            _validator = validator;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Gestion tipos
        
        public ActionResult Tipos()
        {
            var motos = _tipoService.ObtenerTipos();
            return View(motos);
        }



        [HttpPost]
        public ActionResult registrarTipos(Tipo objeto)
        {
            if (ModelState.IsValid)
            {
                var registroExitoso = _tipoService.registrarTipo(objeto);
                if (registroExitoso)
                {
                    return RedirectToAction("Tipos");
                }
                else
                {
                    ModelState.AddModelError("", "Error al registrar. Inténtelo nuevamente.");
                }
            }

            // Si hay error, volver a cargar la lista de tipos
            var tipos = _tipoService.ObtenerTipos();
            return View("Tipos", tipos);
        }


        //public ActionResult registrarTipos(Tipo objeto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var registroExitoso = _tipoService.registrarTipo(objeto);
        //        if (registroExitoso)
        //        {
        //            return RedirectToAction("Tipos");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Error al registrar. Inténtelo nuevamente.");
        //        }
        //    }

        //    // Recargar la lista de Tipos si la validación falla
        //    var tipos = _tipoService.ObtenerTipos(); // Debes tener un método para obtener la lista

        //    return View("Tipos", tipos);
        //}


        [HttpGet]
        public ActionResult EditTipo(int id)
        {
            var tipos = _tipoService.BuscarPorID(id);
            if (tipos == null)
            {
                return NotFound();
            }
            return View(tipos);
        }

        [HttpPost]
        public ActionResult EditTipo(Tipo objeto)
        {
            if (ModelState.IsValid)
            {
                _tipoService.Actualizar(objeto);
                return RedirectToAction("Tipos"); 
            }
            return View(objeto);
        }

        // Eliminar Tipo

        public ActionResult EliminarTipo(int id)
        {

            bool query = _tipoService.Eliminar(id);
            if (query)
            {
                TempData["Mensaje"] = "Tipo eliminado correctamente.";
            }
            else
            {
                TempData["Mensaje"] = "Error al eliminar el tipo o el tipo no existe.";
            }

            return RedirectToAction("Tipos");
        }

        // Marcas
        //Gestion MARCAS

        public ActionResult Marcas()
        {
            var motos = _marcasService.ObtenerMarcas();
            return View(motos);
        }

        [HttpPost]
        public ActionResult registrarMarcas(Marca objeto)
        {
            if (ModelState.IsValid)
            {
                var registroExitoso = _marcasService.registrarMarcas(objeto);
                if (registroExitoso)
                {
                    return RedirectToAction("Marcas");
                }
                else
                {
                    ModelState.AddModelError("", "Error al registrar. Inténtelo nuevamente.");
                }
            }
            return View(objeto);
        }

        [HttpGet]
        public ActionResult EditMarcas(int id)
        {
            var tipos = _marcasService.BuscarPorID(id);
            if (tipos == null)
            {
                return NotFound();
            }
            return View(tipos);
        }

        

        [HttpPost]
        public ActionResult EditMarcas(Marca objeto)
        {
            if (ModelState.IsValid)
            {
                _marcasService.Actualizar(objeto);

                return RedirectToAction("Marcas");
            }
            return View(objeto);
        }




        // Eliminar Marcas

        public ActionResult EliminarMarcas(int id)
        {

            bool query = _marcasService.Eliminar(id);
            if (query)
            {
                TempData["Mensaje"] = "Eliminado correctamente.";
            }
            else
            {
                TempData["Mensaje"] = "Error al eliminar.";
            }

            return RedirectToAction("Marcas");
        }

        // -------------------------------------------- Modelos
        public ActionResult Modelos()
        {
            var tipos = _tipoService.ObtenerTipos();
            ViewBag.Tipos = tipos;

            if (tipos != null)
            {
                //foreach (var tipo in tipos)
                //{
                //    Console.WriteLine($"Tipo: {tipo.Tipo1}");
                //}
            }
            else
            {
                Console.WriteLine("ObtenerTipos() devolvió NULL");
            }
            var marcas = _marcasService.ObtenerMarcas();
            ViewBag.Marcas = marcas;

            if (marcas != null)
            {
                //foreach (var marca in marcas)
                //{
                //    Console.WriteLine($"Marcas: {marca.NombreMarca}");
                //}
            }
            else
            {
                Console.WriteLine("ObtenerTipos() devolvió NULL");
            }


            var modelos = _modelosService.ObtenerModelos();
            return View(modelos);
        }

        [HttpPost]
        public ActionResult RegistrarModelos(Modelo objeto)
        {

            Console.WriteLine("DEBUGEANDO:::: "+objeto.IdTipo +" | "  + objeto.IdMarca + " | " + objeto.NombreModelo);

            // Validamos que IdTipo y IdMarca sean valores válidos (mayores a 0)
            if (objeto.IdTipo <= 0 || objeto.IdMarca <= 0)
            {
                ModelState.AddModelError("", "Debe seleccionar un Tipo y una Marca válidos.");
            }

            // Validamos el estado del modelo
            if (!ModelState.IsValid)
            {
                Console.WriteLine("FLUJO DE MODELO INVALIDO - Errores encontrados:");

                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        Console.WriteLine($"Campo: {key}, Error: {error.ErrorMessage}");
                    }
                }

                // Recargamos los datos de los selectores para evitar errores en la vista
                TempData["Error"] = "Hubo un error al registrar el modelo. Verifique los campos.";
                return RedirectToAction("Modelos");
            }



            // Intentamos registrar el modelo
            var registroExitoso = _modelosService.RegistrarModelos(objeto);
            Console.WriteLine("ESTADO REGISTRO:::: " + registroExitoso);
            if (registroExitoso)
            {
                return RedirectToAction("Modelos");
            }
            else
            {
                ModelState.AddModelError("", "Error al registrar. Inténtelo nuevamente.");

                return View(objeto);
            }
        }

        [HttpGet]
        public ActionResult EditModelos(int id)
        {
            var modelos = _modelosService.BuscarPorID(id);
            var tipos = _tipoService.ObtenerTipos();
            ViewBag.Tipos = tipos;
            var marcas = _marcasService.ObtenerMarcas();
            ViewBag.Marcas = marcas;
            if (modelos == null)
            {
                return NotFound();
            }
            return View(modelos);
        }

        [HttpPost]
        public ActionResult EditModelos(Modelo objeto)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("DEBUGGGG -------------| NombreModelo"+ objeto.NombreModelo+ "Año"+objeto.Anio + " | IdTipo"+ objeto.IdTipo + " | IdMarca" + objeto.IdMarca);
                _modelosService.Actualizar(objeto);

                return RedirectToAction("Modelos");
            }
            return View(objeto);
        }








        // --------- PRUEBA DE MODAL CON MODELOS |||||||| REVISAR MAÑANA, AHORA NO PUEDO
        [HttpPost]
        public IActionResult AñadirDetalle(int id)
        {
            Console.WriteLine("PROBANDOOOOO : " + id);
            return RedirectToAction("Detalles", new { id = id });
        }

        [HttpPost]
        public ActionResult Detalles(int id)
        {
            Console.WriteLine("DETALLES ID :::: "  + id);
            return View();
        }



        // ------------ END
        // Eliminar Modelo|

        public ActionResult EliminarModelos(int id)
        {
            bool query = _modelosService.Eliminar(id);
            if (query)
            {
                TempData["Mensaje"] = "Eliminado correctamente.";
            }
            else
            {
                TempData["Mensaje"] = "Error al eliminar.";
            }

            return RedirectToAction("Modelos");
        }

        // -------------------------------------------- Motos
        public ActionResult Motos()
        {
            return View(_motosService.ListarMotos());
        }















        // PROBANDO ACCION

        
        
        [HttpGet]
        public ActionResult MMotos()
        {
            var tipos = _tipoService.ObtenerTipos();
            ViewBag.Tipos = tipos;

            if (tipos != null)
            {
                foreach (var tipo in tipos)
                {
                    Console.WriteLine($"||||||||||||Tipo: {tipo.Tipo1}");
                }
            }
            else
            {
                Console.WriteLine("ObtenerTipos() devolvió NULL");
            }
            var marcas = _marcasService.ObtenerMarcas();
            ViewBag.Marcas = marcas;

            if (marcas != null)
            {
                foreach (var marca in marcas)
                {
                    Console.WriteLine($"||||||||||||||Marcas: {marca.NombreMarca}");
                }
            }
            else
            {
                Console.WriteLine("ObtenerTipos() devolvió NULL");
            }

            return View();
        }

        [HttpPost]
        public ActionResult MMotos(MotoModeloViewModel viewModel)
        {
            Console.WriteLine("****************Antes del modelstate");
            if (ModelState.IsValid)
            {
            Console.WriteLine("------------------------------DENTRO DEL MODELSTATE");
                Console.WriteLine("DEBUGEANDO PUSH:::::::::::::::"+viewModel.NombreModelo+ " | " + viewModel.Precio + " | " + viewModel.VelocidadMax );
                // Crear el modelo usando el service
                var modelo = _modelosService.CrearModelo(viewModel);

                // Crear la moto usando el service y pasando el ID del modelo creado
                var moto = _motosService.CrearMoto(viewModel, modelo.IdModelo);

                _fotoService.InsertarFoto(viewModel, moto.IdMoto);
                // Redirigir o mostrar mensaje de éxito
                return RedirectToAction("Modelos");
                //return RedirectToAction("Modelos", new { id = modelo.IdModelo });
            }

            // Si el modelo no es válido, devolver el formulario con errores
            return View("Admin");
        }


    }
}
