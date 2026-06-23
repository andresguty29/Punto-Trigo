using Abstracciones.Interfaces.API.UsuarioAPI;
using Abstracciones.Interfaces.Flujo.Usuario;
using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Usuario.Usuario;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase, IUsuarioController
    {
        private readonly IUsuarioFlujo _usuarioFlujo;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioFlujo usuarioFlujo, ILogger<UsuarioController> logger)
        {
            _usuarioFlujo = usuarioFlujo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _usuarioFlujo.Obtener();
            if (!resultado.Any()) return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener(Guid Id)
        {
            var resultado = await _usuarioFlujo.Obtener(Id);
            if (resultado == null) return NotFound();
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(UsuarioRequest usuario)
        {
            try
            {
                var resultado = await _usuarioFlujo.Agregar(usuario);
                return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Editar(Guid Id, UsuarioRequest usuario)
        {
            try
            {
                var resultado = await _usuarioFlujo.Editar(Id, usuario);
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
            await _usuarioFlujo.Eliminar(Id);
            return NoContent();
        }

        [HttpPatch("{Id}/activar")]
        public async Task<IActionResult> Activar(Guid Id)
        {
            await _usuarioFlujo.Activar(Id);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var resultado = await _usuarioFlujo.Login(request);
            if (resultado == null)
                return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos." });
            return Ok(resultado);
        }

        [HttpPost("{Id}/cambiar-contrasena")]
        public async Task<IActionResult> CambiarContrasena(Guid Id, CambiarContrasenaRequest request)
        {
            var resultado = await _usuarioFlujo.CambiarContrasena(Id, request);
            if (!resultado)
                return BadRequest(new { mensaje = "La contraseña actual es incorrecta." });
            return NoContent();
        }
    }
}
