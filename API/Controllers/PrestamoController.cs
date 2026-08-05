using Abstracciones.Interfaces.API.PrestamoAPI;
using Abstracciones.Interfaces.Flujo.Prestamo;
using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Prestamo.Prestamo;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase, IPrestamoController
    {
        private readonly IPrestamoFlujo _prestamoFlujo;
        private readonly ILogger<PrestamoController> _logger;

        public PrestamoController(IPrestamoFlujo prestamoFlujo, ILogger<PrestamoController> logger)
        {
            _prestamoFlujo = prestamoFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(PrestamoRequest prestamo)
        {
            try
            {
                var resultado = await _prestamoFlujo.Registrar(prestamo);
                return Ok(resultado);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Obtener(Guid idTrabajador)
        {
            var resultado = await _prestamoFlujo.Obtener(idTrabajador);

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }
    }
}
