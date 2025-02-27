using webmotos.Models;

namespace webmotos.Services
{
    public class FotoService
    {
        private readonly WebmotosContext _context;
        // Constructor donde se inyecta el DbContext
        public FotoService(WebmotosContext context)
        {
            _context = context;
        }

        public void InsertarFoto(MotoModeloViewModel viewModel, int idMoto)
        {

            var foto = new Foto
            {
                IdMoto = idMoto,
                UrlFoto = viewModel.UrlFoto
            };

            _context.Fotos.Add(foto);
            _context.SaveChanges();
        }
    }
}
