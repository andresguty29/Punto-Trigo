using Abstracciones.Interfaces.API.PlanillaAPI;
using Abstracciones.Interfaces.Flujo.Planilla;
using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Planilla.Planilla;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanillaController : ControllerBase, IPlanillaController
    {
        private readonly IPlanillaFlujo _planillaFlujo;
        private readonly ILogger<PlanillaController> _logger;

        public PlanillaController(IPlanillaFlujo planillaFlujo, ILogger<PlanillaController> logger)
        {
            _planillaFlujo = planillaFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> GenerarDetallePago(GenerarDetallePagoRequest request)
        {
            try
            {
                var resultado = await _planillaFlujo.GenerarDetallePago(request);
                return CreatedAtAction(nameof(ObtenerDetalle), new { Id = resultado }, resultado);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerDetalle(Guid Id)
        {
            var resultado = await _planillaFlujo.ObtenerDetalle(Id);

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerHistorial(DateOnly? fechaInicio, DateOnly? fechaFin)
        {
            var resultado = await _planillaFlujo.ObtenerHistorial(fechaInicio, fechaFin);

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }
    }
}
