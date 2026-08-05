using Abstracciones.Interfaces.API.RegistroAccesoAPI;
using Abstracciones.Interfaces.Flujo.RegistroAcceso;
using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.RegistroAcceso.RegistroAcceso;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroAccesoController : ControllerBase, IRegistroAccesoController
    {
        private readonly IRegistroAccesoFlujo _registroAccesoFlujo;
        private readonly ILogger<RegistroAccesoController> _logger;

        public RegistroAccesoController(IRegistroAccesoFlujo registroAccesoFlujo, ILogger<RegistroAccesoController> logger)
        {
            _registroAccesoFlujo = registroAccesoFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarAccesoRequest registro)
        {
            var resultado = await _registroAccesoFlujo.Registrar(registro);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, resultado);
        }

        [HttpPatch("{Id}/cerrar")]
        public async Task<IActionResult> CerrarSesion(Guid Id)
        {
            await _registroAccesoFlujo.CerrarSesion(Id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Obtener(DateOnly? fechaInicio, DateOnly? fechaFin, string? nombreUsuario)
        {
            var resultado = await _registroAccesoFlujo.Obtener(fechaInicio, fechaFin, nombreUsuario);

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener(Guid Id)
        {
            var resultado = await _registroAccesoFlujo.Obtener(Id);

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }
    }
}
