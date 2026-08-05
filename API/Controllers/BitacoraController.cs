using Abstracciones.Interfaces.API.BitacoraAPI;
using Abstracciones.Interfaces.Flujo.Bitacora;
using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Bitacora.Bitacora;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitacoraController : ControllerBase, IBitacoraController
    {
        private readonly IBitacoraFlujo _bitacoraFlujo;
        private readonly ILogger<BitacoraController> _logger;

        public BitacoraController(IBitacoraFlujo bitacoraFlujo, ILogger<BitacoraController> logger)
        {
            _bitacoraFlujo = bitacoraFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarBitacoraRequest registro)
        {
            var resultado = await _bitacoraFlujo.Registrar(registro);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> Obtener(DateOnly? fechaInicio, DateOnly? fechaFin)
        {
            var resultado = await _bitacoraFlujo.Obtener(fechaInicio, fechaFin);

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }
    }
}
