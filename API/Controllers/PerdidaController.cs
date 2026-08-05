using Abstracciones.Interfaces.API.PerdidaAPI;
using Abstracciones.Interfaces.Flujo.Perdida;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerdidaController : ControllerBase, IPerdidaController
    {
        private readonly IPerdidaFlujo _perdidaFlujo;
        private readonly ILogger<PerdidaController> _logger;

        public PerdidaController(IPerdidaFlujo perdidaFlujo, ILogger<PerdidaController> logger)
        {
            _perdidaFlujo = perdidaFlujo;
            _logger = logger;
        }

        [HttpGet("pendientes")]
        public async Task<IActionResult> ObtenerPendientes()
        {
            var resultado = await _perdidaFlujo.ObtenerPendientes();

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        [HttpPatch("{idMovimiento}/procesar")]
        public async Task<IActionResult> Procesar(Guid idMovimiento)
        {
            try
            {
                var resultado = await _perdidaFlujo.Procesar(idMovimiento);
                return Ok(resultado);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }
    }
}
