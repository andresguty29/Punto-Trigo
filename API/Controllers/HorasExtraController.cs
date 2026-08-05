using Abstracciones.Interfaces.API.HorasExtraAPI;
using Abstracciones.Interfaces.Flujo.HorasExtra;
using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.HorasExtra.HorasExtra;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorasExtraController : ControllerBase, IHorasExtraController
    {
        private readonly IHorasExtraFlujo _horasExtraFlujo;
        private readonly ILogger<HorasExtraController> _logger;

        public HorasExtraController(IHorasExtraFlujo horasExtraFlujo, ILogger<HorasExtraController> logger)
        {
            _horasExtraFlujo = horasExtraFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(HorasExtraRequest horasExtra)
        {
            try
            {
                var resultado = await _horasExtraFlujo.Registrar(horasExtra);
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
            var resultado = await _horasExtraFlujo.Obtener(idTrabajador);

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }
    }
}
