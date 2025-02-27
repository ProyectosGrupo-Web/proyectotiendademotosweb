using Microsoft.EntityFrameworkCore;
using webmotos.Models;

namespace webmotos.Services
{
    public class ModelosService
    {
        private readonly WebmotosContext _context;

        public ModelosService(WebmotosContext context)
        {
            _context = context;
        }

        public List<Modelo> ObtenerModelos()
        {
            var query = new List<Modelo>();
            try
            {
                query = _context.Modelos
                    .Include(m=> m.IdMarcaNavigation)
                    .Include(t=>t.IdTipoNavigation)
                    .ToList();
            }
            catch (Exception) {
                throw;
            }

            return _context.Modelos.ToList();
        }

        public bool RegistrarModelos(Modelo objeto)
        {
            try
            {
                _context.Modelos.Add(objeto);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Modelo BuscarPorID(int id)
        {
            return _context.Modelos.Find(id);
        }

        public Modelo ObtenerDaatosPorID(int id) {
            try
            {
                var objeto = _context.Modelos.Find(id);
                if (objeto != null) { 
                    
                }
                return objeto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Actualizar(Modelo objeto)
        {
            if (objeto == null)
            {
                Console.WriteLine("ERROR: El objeto recibido es nulo.");
                return false;
            }

            try
            {
                var entidad = _context.Modelos.Find(objeto.IdModelo);

                if (entidad == null)
                {
                    Console.WriteLine($"ERROR: NO ENCONTRADO. ID: {objeto.IdModelo}");
                    return false;
                }

                // Actualizar valores
                entidad.NombreModelo = objeto.NombreModelo;
                entidad.IdMarca = objeto.IdMarca;
                entidad.IdTipo = objeto.IdTipo;
                entidad.Anio = objeto.Anio;

                _context.SaveChanges(); // Guardar cambios en la BD
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR al actualizar el modelo: {ex.Message}");
                return false;
            }
        }


        public bool Eliminar(int id)
        {
            try
            {
                var obj = _context.Modelos.Find(id);
                if (obj == null)
                {
                    return false;
                }

                _context.Modelos.Remove(obj);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Registrar el error (log)
                Console.WriteLine($"Error al eliminar el objeto con ID {id}: {ex.Message}");
                return false;
            }
        }

        // METODO DE PRUEBA PARA VIEWMODEL SEPARANDOOLOOO
        public Modelo CrearModelo(MotoModeloViewModel viewModel)
        {
            var modelo = new Modelo
            {
                NombreModelo = viewModel.NombreModelo,
                Anio = viewModel.Anio,
                IdTipo = viewModel.IdTipo,
                IdMarca = viewModel.IdMarca,
                CreadoEn = DateTime.Now
            };

            _context.Modelos.Add(modelo);
            _context.SaveChanges();

            return modelo;
        }



    }
}
