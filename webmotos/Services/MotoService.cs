using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webmotos.Models;

namespace webmotos.Services
{
    public class MotoService
    {
        private readonly WebmotosContext _context;
        // Constructor donde se inyecta el DbContext
        public MotoService(WebmotosContext context)
        {
            _context = context;
        }



        //METODOS
        // Logica de negocio

        public List<Moto> ListarMotos()
        {
            try
            {
                return _context.Motos
                    .Include(m => m.IdModeloNavigation)
                    .AsNoTracking() // Abre la conexion en modo lectura
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ListarMotos: {ex.Message}");
                throw;
            }
        }

        public List<Moto> FiltrarMotosPorTipo(string tipo)
        {
            var query = new List<Moto>();
            try
            {
                if (tipo != "")
                {
                    var idtipo = _context.Tipos.Where(t => t.Tipo1 == tipo).FirstOrDefault();
                    query = _context.Motos
                                .Include(m => m.IdModeloNavigation)
                                .Include(m => m.Fotos)
                                .Where(d => d.IdModeloNavigation.IdTipoNavigation.Tipo1 == tipo)
                                .ToList();
                }
                else
                {
                    query = _context.Motos
                                .Include(m => m.IdModeloNavigation)
                                .Include(m => m.Fotos)
                                .ToList();
                }
            }
            catch (Exception)
            {
                
            }
            return query;
        }

        public Moto detalleMoto(int id)
        {
            try
            {
                var moto = _context.Motos
                    .Where(x => x.IdMoto == id)
                    .Include(m => m.IdModeloNavigation)
                    .Include(m => m.Fotos)
                    .FirstOrDefault(); // Obtiene solo un objeto o null
                return moto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
                throw;
            }
        }
        public Moto CrearMoto(MotoModeloViewModel viewModel, int idModelo)
        {
            viewModel.Disponible = true;
            var moto = new Moto
            {
                IdModelo = idModelo, // Asociar el modelo creado
                Precio = viewModel.Precio,
                Color = viewModel.Color,
                Cilindrada = viewModel.Cilindrada,
                Potencia = viewModel.Potencia,
                velocidadMax = viewModel.VelocidadMax,
                Descripcion = viewModel.Descripcion,
                Disponible = viewModel.Disponible,
                CreadoEn = DateTime.Now
            };

            _context.Motos.Add(moto);
            _context.SaveChanges();
            return moto;
        }

    }
}
