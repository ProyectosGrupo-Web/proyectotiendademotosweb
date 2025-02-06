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

        
        //public List<Moto> filtrarPorTipo(string tipo)
        //{
        //    var query = new List<Moto>();
        //    try
        //    {
        //        query = _context.Motos
        //        .Include(m => m.IdModeloNavigation)
        //        .ThenInclude(mod => mod.IdTipoNavigation)
        //        .Where(m => m.IdModeloNavigation.IdTipoNavigation.Tipo1 == tipo)
        //        .ToList();
        //    }
        //    catch { 
        //    }
        //    return query;
            
        //}
        

        public List<Moto> FiltrarMotosPorTipo(string tipo)
        {
            var query = new List<Moto>();
            Console.WriteLine("SE FILTRARA POR :::: "+tipo);
            try
            {
                if (tipo != "")
                {
                    var idtipo = _context.Tipos.Where(t => t.Tipo1 == tipo).FirstOrDefault();
                    Console.WriteLine("IDTIPO:"+idtipo.IdTipo);
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
                
                foreach(var item in query)
                {
                    Console.WriteLine($"TIPO : {item.IdModeloNavigation.IdTipoNavigation.Tipo1}");

                }
            }
            catch (Exception)
            {
                
            }
            return query;
        }


        public List<Moto> listarMotos()
        {
            var query = new List<Moto>();
            try
            {

                using (var db = new WebmotosContext())
                {
                    query = db.Motos.ToList();
                }
            }
            catch (Exception) { throw; }
            return query;
        }

        //public List<Moto> listarMotosConFotos()
        //{
        //    var query = new List<Moto>(); // Inicializar la lista vacía
        //    try
        //    {
        //        // Usamos el contexto inyectado (sin crear una nueva instancia)
        //        query = _context.Motos
        //                      .Include(m => m.IdModeloNavigation)
        //                      .Include(m => m.Fotos)
        //                      .ToList();

        //        var sqlQuery = _context.Motos.ToQueryString();
        //        Console.WriteLine($"SQL Generado: {sqlQuery}");
        //    }
        //    catch (Exception) { throw; }

        //    return query; // Devuelve una lista vacía en caso de error
        //}




    }
}
