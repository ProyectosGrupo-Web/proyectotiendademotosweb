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

    }
}
