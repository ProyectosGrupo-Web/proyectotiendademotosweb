using webmotos.Models;

namespace webmotos.Services
{
    public class MarcasService
    {
        private readonly WebmotosContext _context;

        public MarcasService(WebmotosContext context)
        {
            _context = context;
        }

        public List<Marca> ObtenerMarcas()
        {
            return _context.Marcas.ToList();
        }

        public bool registrarMarcas(Marca objeto)
        {
            try
            {
                _context.Marcas.Add(objeto);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Marca BuscarPorID(int id)
        {
            return _context.Marcas.Find(id);
        }

        public bool Actualizar(Marca objeto)
        {
            var entidad = _context.Marcas.FirstOrDefault(t => t.IdMarca == objeto.IdMarca);
            if (entidad == null)
            {
                Console.WriteLine($"ERROR: NO ENCONTRADO. ID: {objeto.IdMarca}");
                return false;
            }
            entidad.NombreMarca = objeto.NombreMarca;
            entidad.PaisOrigen = objeto.PaisOrigen;
            _context.SaveChanges();
            return true;
        }

        public bool Eliminar(int id)
        {
            try
            {
                var obj = _context.Marcas.Find(id);
                if (obj == null)
                {
                    return false;
                }

                _context.Marcas.Remove(obj);
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
    }
}
