using Abstracciones.Interfaces.API.ReporteAPI;
using Abstracciones.Interfaces.Flujo.Reporte;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase, IReporteController
    {
        private readonly IReporteFlujo _reporteFlujo;
        private readonly ILogger<ReporteController> _logger;

        public ReporteController(IReporteFlujo reporteFlujo, ILogger<ReporteController> logger)
        {
            _reporteFlujo = reporteFlujo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener(DateOnly fechaInicio, DateOnly fechaFin)
        {
            try
            {
                var resultado = await _reporteFlujo.Obtener(fechaInicio, fechaFin);
                return Ok(resultado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
