using Abstracciones.Interfaces.API.InventarioAPI;
using Abstracciones.Interfaces.Flujo.Inventario;
using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Inventario.Inventario;
using static Abstracciones.Modelos.Inventario.Movimiento;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase, IInventarioController
    {
        private readonly IInventarioFlujo _inventarioFlujo;
        private readonly ILogger<InventarioController> _logger;

        public InventarioController(IInventarioFlujo inventarioFlujo, ILogger<InventarioController> logger)
        {
            _inventarioFlujo = inventarioFlujo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _inventarioFlujo.Obtener();
            if (!resultado.Any()) return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener(Guid Id)
        {
            var resultado = await _inventarioFlujo.Obtener(Id);
            if (resultado == null) return NotFound();
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(InventarioRequest inventario)
        {
            var resultado = await _inventarioFlujo.Agregar(inventario);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Editar(Guid Id, InventarioRequest inventario)
        {
            var resultado = await _inventarioFlujo.Editar(Id, inventario);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar(Guid Id)
        {
            await _inventarioFlujo.Eliminar(Id);
            return NoContent();
        }

        [HttpPatch("{Id}/activar")]
        public async Task<IActionResult> Activar(Guid Id)
        {
            await _inventarioFlujo.Activar(Id);
            return NoContent();
        }

        [HttpPost("movimiento")]
        public async Task<IActionResult> RegistrarMovimiento(MovimientoRequest movimiento)
        {
            var resultado = await _inventarioFlujo.RegistrarMovimiento(movimiento);
            return Ok(resultado);
        }

        [HttpGet("{Id}/movimientos")]
        public async Task<IActionResult> ObtenerMovimientos(Guid Id)
        {
            var resultado = await _inventarioFlujo.ObtenerMovimientos(Id);
            if (!resultado.Any()) return NoContent();
            return Ok(resultado);
        }
    }
}
