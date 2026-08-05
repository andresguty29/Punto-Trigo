using Abstracciones.Interfaces.API.VacacionAPI;
using Abstracciones.Interfaces.Flujo.Vacacion;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacacionController : ControllerBase, IVacacionController
    {
        private readonly IVacacionFlujo _vacacionFlujo;
        private readonly ILogger<VacacionController> _logger;

        public VacacionController(IVacacionFlujo vacacionFlujo, ILogger<VacacionController> logger)
        {
            _vacacionFlujo = vacacionFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Asignar(Guid idTrabajador)
        {
            try
            {
                var resultado = await _vacacionFlujo.Asignar(idTrabajador);
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
            var resultado = await _vacacionFlujo.Obtener(idTrabajador);

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }
    }
}
