using Abstracciones.Interfaces.API.CompraAPI;
using Abstracciones.Interfaces.Flujo.Compra;
using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Compra.Compra;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase, ICompraController
    {
        private readonly ICompraFlujo _compraFlujo;
        private readonly ILogger<CompraController> _logger;

        public CompraController(ICompraFlujo compraFlujo, ILogger<CompraController> logger)
        {
            _compraFlujo = compraFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(CompraRequest compra)
        {
            try
            {
                var resultado = await _compraFlujo.Agregar(compra);
                return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPatch("{Id}/anular")]
        public async Task<IActionResult> Anular(Guid Id)
        {
            await _compraFlujo.Anular(Id);
            return NoContent();
        }

        [HttpPatch("{Id}/reclasificar")]
        public async Task<IActionResult> Reclasificar(Guid Id, ReclasificarCompraRequest reclasificacion)
        {
            try
            {
                var resultado = await _compraFlujo.Reclasificar(Id, reclasificacion);
                return Ok(resultado);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Obtener(DateOnly? fechaInicio, DateOnly? fechaFin, decimal? montoMinimo)
        {
            try
            {
                var resultado = await _compraFlujo.Obtener(fechaInicio, fechaFin, montoMinimo);

                if (!resultado.Any())
                    return NoContent();

                return Ok(resultado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener(Guid Id)
        {
            var resultado = await _compraFlujo.Obtener(Id);

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }

        [HttpGet("{Id}/detalle")]
        public async Task<IActionResult> ObtenerDetalle(Guid Id)
        {
            var resultado = await _compraFlujo.ObtenerDetalle(Id);

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }
    }
}
