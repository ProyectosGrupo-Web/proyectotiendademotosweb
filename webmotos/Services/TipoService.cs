using Microsoft.EntityFrameworkCore;
using webmotos.Models;

namespace webmotos.Services
{
    public class TipoService
    {
        private readonly WebmotosContext _context;

        public TipoService(WebmotosContext context)
        {
            _context = context;
        }

        public List<Tipo> ObtenerTipos()
        {
            return _context.Tipos.ToList();
        }

        public bool registrarTipo(Tipo objeto)
        {
            try
            {
                _context.Tipos.Add(objeto);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Tipo BuscarPorID(int id)
        {
            return _context.Tipos.Find(id);
        }

        public bool Actualizar(Tipo objeto)
        {
            var entidad = _context.Tipos.FirstOrDefault(t => t.IdTipo == objeto.IdTipo);
            if (entidad == null)
            {
                Console.WriteLine($"ERROR: NO ENCONTRADO. ID: {objeto.IdTipo}");
                return false;
            }
            entidad.Tipo1 = objeto.Tipo1;
            _context.SaveChanges();
            return true;
        }

        public bool Eliminar(int id)
        {
            try
            {
                var obj = _context.Tipos.Find(id);
                if (obj == null)
                {
                    return false; 
                }

                _context.Tipos.Remove(obj);
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
