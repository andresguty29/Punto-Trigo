using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PerdidaController : Controller
    {
        private readonly PerdidaService _perdidaService;
        private readonly BitacoraService _bitacoraService;

        public PerdidaController(PerdidaService perdidaService, BitacoraService bitacoraService)
        {
            _perdidaService = perdidaService;
            _bitacoraService = bitacoraService;
        }

        [HttpGet]
        public async Task<IActionResult> Historial()
        {
            var pendientes = await _perdidaService.ObtenerPendientes();
            return View(pendientes);
        }

        [HttpPost]
        public async Task<IActionResult> Procesar(Guid idMovimiento)
        {
            var (ok, error) = await _perdidaService.Procesar(idMovimiento);

            if (ok)
            {
                await _bitacoraService.Registrar(User, "ProcesarPerdidaVencimiento", $"Procesó como pérdida el movimiento {idMovimiento} por vencimiento.");
                TempData["PerdidaOk"] = "Producto vencido procesado como pérdida correctamente.";
            }
            else
            {
                TempData["PerdidaError"] = error ?? "No se pudo procesar el vencimiento.";
            }

            return RedirectToAction(nameof(Historial));
        }
    }
}
