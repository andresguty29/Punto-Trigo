using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PlanillaController : Controller
    {
        private readonly PlanillaService _planillaService;
        private readonly BitacoraService _bitacoraService;

        public PlanillaController(PlanillaService planillaService, BitacoraService bitacoraService)
        {
            _planillaService = planillaService;
            _bitacoraService = bitacoraService;
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(Guid id)
        {
            var detalle = await _planillaService.ObtenerDetalle(id);
            if (detalle == null) return NotFound();

            return View(detalle);
        }

        [HttpGet]
        public async Task<IActionResult> Historial(DateOnly? fechaInicio, DateOnly? fechaFin)
        {
            var (items, error) = await _planillaService.ObtenerHistorial(fechaInicio, fechaFin);

            await _bitacoraService.Registrar(User, "ConsultarHistorialSalarios", $"Consultó el historial de salarios ({fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}).");

            ViewBag.FechaInicio = fechaInicio;
            ViewBag.FechaFin = fechaFin;
            ViewBag.ErrorApi = error;

            return View(items);
        }
    }
}
