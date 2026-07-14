using Abstracciones.Interfaces.API.TiqueteAPI;
using Abstracciones.Interfaces.Flujo.Tiquete;
using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Tiquete.Tiquete;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiqueteController : ControllerBase, ITiqueteController
    {
        private readonly ITiqueteFlujo _tiqueteFlujo;
        private readonly ILogger<TiqueteController> _logger;

        public TiqueteController(ITiqueteFlujo tiqueteFlujo, ILogger<TiqueteController> logger)
        {
            _tiqueteFlujo = tiqueteFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(TiqueteRequest tiquete, Guid? idTrabajador)
        {
            try
            {
                var resultado = await _tiqueteFlujo.Agregar(tiquete, idTrabajador);
                return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPatch("{Id}/reintentar")]
        public async Task<IActionResult> ReintentarEnvio(Guid Id)
        {
            try
            {
                var resultado = await _tiqueteFlujo.ReintentarEnvio(Id);
                return Ok(resultado);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _tiqueteFlujo.Obtener();

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener(Guid Id)
        {
            var resultado = await _tiqueteFlujo.Obtener(Id);

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }

        [HttpGet("{Id}/detalle")]
        public async Task<IActionResult> ObtenerDetalle(Guid Id)
        {
            var resultado = await _tiqueteFlujo.ObtenerDetalle(Id);

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }
    }
}
