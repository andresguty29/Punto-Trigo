using Abstracciones.Interfaces.API.ClienteAPI;
using Abstracciones.Interfaces.Flujo.Cliente;
using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Cliente.Cliente;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase, IClienteController
    {
        private IClienteFlujo _clienteFlujo;
        private ILogger<ClienteController> _logger;

        public ClienteController(IClienteFlujo clienteFlujo, ILogger<ClienteController> logger)
        {
            _clienteFlujo = clienteFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(ClienteRequest cliente)
        {
            try
            {
                var resultado = await _clienteFlujo.Agregar(cliente);
                return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Editar(Guid Id, ClienteRequest cliente)
        {
            try
            {
                var resultado = await _clienteFlujo.Editar(Id, cliente);
                return Ok(resultado);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar(Guid Id)
        {
            var resultado = await _clienteFlujo.Eliminar(Id);
            return NoContent();
        }

        [HttpPatch("{Id}/activar")]
        public async Task<IActionResult> Activar(Guid Id)
        {
            await _clienteFlujo.Activar(Id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _clienteFlujo.Obtener();

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener(Guid Id)
        {
            var resultado = await _clienteFlujo.Obtener(Id);

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }

        [HttpGet("cedula/{cedula}")]
        public async Task<IActionResult> ObtenerPorCedula(string cedula)
        {
            var resultado = await _clienteFlujo.ObtenerPorCedula(cedula);

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }
    }
}
