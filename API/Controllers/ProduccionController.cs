using Abstracciones.Interfaces.API.ProduccionAPI;
using Abstracciones.Interfaces.Flujo.Produccion;
using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Produccion.Produccion;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduccionController : ControllerBase, IProduccionController
    {
        private readonly IProduccionFlujo _produccionFlujo;
        private readonly ILogger<ProduccionController> _logger;

        public ProduccionController(IProduccionFlujo produccionFlujo, ILogger<ProduccionController> logger)
        {
            _produccionFlujo = produccionFlujo;
            _logger = logger;
        }

        [HttpGet("asignaciones")]
        public async Task<IActionResult> ObtenerAsignaciones()
        {
            var resultado = await _produccionFlujo.ObtenerAsignaciones();
            if (!resultado.Any()) return NoContent();
            return Ok(resultado);
        }

        [HttpGet("asignaciones/{Id}")]
        public async Task<IActionResult> ObtenerAsignacion(Guid Id)
        {
            var resultado = await _produccionFlujo.ObtenerAsignacion(Id);
            if (resultado == null) return NotFound();
            return Ok(resultado);
        }

        [HttpPost("asignaciones")]
        public async Task<IActionResult> AgregarAsignacion(AsignacionRequest asignacion)
        {
            var resultado = await _produccionFlujo.AgregarAsignacion(asignacion);
            return CreatedAtAction(nameof(ObtenerAsignacion), new { Id = resultado }, null);
        }

        [HttpPut("asignaciones/{Id}")]
        public async Task<IActionResult> EditarAsignacion(Guid Id, AsignacionRequest asignacion)
        {
            var resultado = await _produccionFlujo.EditarAsignacion(Id, asignacion);
            return Ok(resultado);
        }

        [HttpDelete("asignaciones/{Id}")]
        public async Task<IActionResult> EliminarAsignacion(Guid Id)
        {
            await _produccionFlujo.EliminarAsignacion(Id);
            return NoContent();
        }

        [HttpPatch("asignaciones/{Id}/activar")]
        public async Task<IActionResult> ActivarAsignacion(Guid Id)
        {
            await _produccionFlujo.ActivarAsignacion(Id);
            return NoContent();
        }

        [HttpGet("lista-diaria")]
        public async Task<IActionResult> ObtenerListaDiaria([FromQuery] Guid? Id_Trabajador)
        {
            var resultado = await _produccionFlujo.ObtenerListaDiaria(Id_Trabajador);
            if (!resultado.Any()) return NoContent();
            return Ok(resultado);
        }

        [HttpGet("trabajadores/{Id_Trabajador}/productos")]
        public async Task<IActionResult> ObtenerProductosAsignados(Guid Id_Trabajador)
        {
            var resultado = await _produccionFlujo.ObtenerProductosAsignados(Id_Trabajador);
            if (!resultado.Any()) return NoContent();
            return Ok(resultado);
        }

        [HttpGet("asignaciones/{Id_Asignacion}/materiales")]
        public async Task<IActionResult> ObtenerMateriales(Guid Id_Asignacion)
        {
            var resultado = await _produccionFlujo.ObtenerMateriales(Id_Asignacion);
            if (!resultado.Any()) return NoContent();
            return Ok(resultado);
        }

        [HttpPatch("asignaciones/{Id_Asignacion}/realizar")]
        public async Task<IActionResult> MarcarRealizada(Guid Id_Asignacion)
        {
            await _produccionFlujo.MarcarRealizada(Id_Asignacion);
            return NoContent();
        }
    }
}
