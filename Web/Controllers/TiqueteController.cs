using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin,Cajas")]
    public class TiqueteController : Controller
    {
        private readonly TiqueteService _tiqueteService;

        public TiqueteController(TiqueteService tiqueteService)
        {
            _tiqueteService = tiqueteService;
        }

        [HttpGet]
        public async Task<IActionResult> Historial()
        {
            var tiquetes = await _tiqueteService.Obtener();
            return View(tiquetes);
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(Guid id)
        {
            var tiquete = await _tiqueteService.Obtener(id);
            if (tiquete == null) return NotFound();

            var detalles = await _tiqueteService.ObtenerDetalle(id);
            ViewBag.Detalles = detalles;
            return View(tiquete);
        }

        [HttpPost]
        public async Task<IActionResult> Reintentar(Guid id)
        {
            await _tiqueteService.ReintentarEnvio(id);
            return RedirectToAction(nameof(Historial));
        }
    }
}
