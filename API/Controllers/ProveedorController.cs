using Abstracciones.Interfaces.API.ProveedorAPI;
using Abstracciones.Interfaces.Flujo.Proveedor;
using Abstracciones.Modelos.Proveedor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase, IProveedorController
    {
        private IProveedorFlujo _proveedorFlujo;
        private ILogger<ProveedorController> _logger;
        public ProveedorController(IProveedorFlujo proveedorFlujo, ILogger<ProveedorController> logger)
        {
            _proveedorFlujo = proveedorFlujo;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Agregar(Proveedor.ProveedorRequest proveedor)
        {
            var resultado = await _proveedorFlujo.Agregar(proveedor);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> Editar(Guid Id, Proveedor.ProveedorRequest proveedor)
        {
            var resultado = await _proveedorFlujo.Editar(Id, proveedor);
            return Ok(resultado);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar(Guid Id)
        {
            var resultado = await _proveedorFlujo.Eliminar(Id);
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _proveedorFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener(Guid Id)
        {
            var resultado = await _proveedorFlujo.Obtener(Id);
            return Ok(resultado);
        }
    }
}
