using Abstracciones.Interfaces.API.TrabajadorAPI;
using Abstracciones.Interfaces.Flujo.Trabajador;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Trabajador.Trabajador;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajadorController : ControllerBase, ITrabajadorController
    {
        private ITrabajadorFlujo _trabajadorFlujo;
        private ILogger<TrabajadorController> _logger;

        public TrabajadorController(ITrabajadorFlujo trabajadorFlujo, ILogger<TrabajadorController> logger)
        {
            _trabajadorFlujo = trabajadorFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(TrabajadorRequest trabajador)
        {
            var resultado = await _trabajadorFlujo.Agregar(trabajador);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Editar(Guid Id, TrabajadorRequest trabajador)
        {
            var resultado = await _trabajadorFlujo.Editar(Id, trabajador);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar(Guid Id)
        {
            var resultado = await _trabajadorFlujo.Eliminar(Id);
            return NoContent();
        }

        [HttpPatch("{Id}/activar")]
        public async Task<IActionResult> Activar(Guid Id)
        {
            await _trabajadorFlujo.Activar(Id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _trabajadorFlujo.Obtener();

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener(Guid Id)
        {
            var resultado = await _trabajadorFlujo.Obtener(Id);

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }

    }
}
