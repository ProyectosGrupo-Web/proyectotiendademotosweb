using Microsoft.EntityFrameworkCore;
using webmotos.Models;
using webmotos.Services;

namespace webmotos.Services
{
    public class HomeService
    {
        private readonly WebmotosContext _context;
        public HomeService(WebmotosContext context) { 
            _context = context;
        }

        public List<Home> listarElementosHome()
        {
            var query = new List<Home>();
            try
            {
                query = _context.Home.ToList();
            }
            catch(Exception) 
            {
            }
            return query;
        }
    }
}


