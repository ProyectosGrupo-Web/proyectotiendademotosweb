using Microsoft.AspNetCore.Mvc;
using webmotos.Services;

namespace webmotos.Components
{
    public class TiposViewComponent : ViewComponent
    {
        private readonly TipoService _tipoService;

        public TiposViewComponent(TipoService tipoService)
        {
            _tipoService = tipoService;
        }
        public IViewComponentResult Invoke()
        {
            var tipos = _tipoService.ObtenerTipos();
            return View(tipos); 
        }
    }
}
