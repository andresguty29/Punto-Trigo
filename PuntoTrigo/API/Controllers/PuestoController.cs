using Abstracciones.Interfaces.API.PuestoAPI;
using Abstracciones.Interfaces.Flujo.Puesto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Puesto.Puesto;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuestoController : ControllerBase, IPuestoController
    {
        private IPuestoFlujo _puestoFlujo;
        private ILogger<PuestoController> _logger;

        public PuestoController(IPuestoFlujo puestoFlujo, ILogger<PuestoController> logger)
        {
            _puestoFlujo = puestoFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(PuestoRequest puesto)
        {
            var resultado = await _puestoFlujo.Agregar(puesto);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Editar(Guid Id, PuestoRequest puesto)
        {
            var resultado = await _puestoFlujo.Editar(Id, puesto);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar(Guid Id)
        {
            var resultado = await _puestoFlujo.Eliminar(Id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _puestoFlujo.Obtener();

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener(Guid Id)
        {
            var resultado = await _puestoFlujo.Obtener(Id);

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }
    }
}
