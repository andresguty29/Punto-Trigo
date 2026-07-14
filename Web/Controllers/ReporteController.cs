using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Web.Services;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReporteController : Controller
    {
        private readonly ReporteService _reporteService;

        public ReporteController(ReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard(DateOnly? fechaInicio, DateOnly? fechaFin)
        {
            var hoy = DateOnly.FromDateTime(DateTime.Today);
            var inicio = fechaInicio ?? hoy.AddDays(-29);
            var fin = fechaFin ?? hoy;

            var (items, error) = await _reporteService.Obtener(inicio, fin);

            ViewBag.FechaInicio = inicio;
            ViewBag.FechaFin = fin;
            ViewBag.ErrorApi = error;

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> ExportarCsv(DateOnly fechaInicio, DateOnly fechaFin)
        {
            var (items, error) = await _reporteService.Obtener(fechaInicio, fechaFin);

            if (error != null)
                return BadRequest(error);

            var sb = new StringBuilder();
            sb.AppendLine("Fecha,Ingresos,Egresos,Utilidad");
            foreach (var dia in items)
            {
                sb.AppendLine($"{dia.Fecha:yyyy-MM-dd},{dia.Ingresos},{dia.Egresos},{dia.Utilidad}");
            }

            var totalIngresos = items.Sum(i => i.Ingresos);
            var totalEgresos = items.Sum(i => i.Egresos);
            sb.AppendLine($"TOTAL,{totalIngresos},{totalEgresos},{totalIngresos - totalEgresos}");

            var bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(sb.ToString())).ToArray();
            return File(bytes, "text/csv", $"reporte_financiero_{fechaInicio:yyyyMMdd}_{fechaFin:yyyyMMdd}.csv");
        }

        [HttpGet]
        public async Task<IActionResult> Imprimir(DateOnly fechaInicio, DateOnly fechaFin)
        {
            var (items, error) = await _reporteService.Obtener(fechaInicio, fechaFin);

            ViewBag.FechaInicio = fechaInicio;
            ViewBag.FechaFin = fechaFin;
            ViewBag.ErrorApi = error;

            return View(items);
        }
    }
}
