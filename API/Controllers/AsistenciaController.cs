using Abstracciones.Interfaces.API.AsistenciaAPI;
using Abstracciones.Interfaces.Flujo.Asistencia;
using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Asistencia.Asistencia;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciaController : ControllerBase, IAsistenciaController
    {
        private readonly IAsistenciaFlujo _asistenciaFlujo;
        private readonly ILogger<AsistenciaController> _logger;

        public AsistenciaController(IAsistenciaFlujo asistenciaFlujo, ILogger<AsistenciaController> logger)
        {
            _asistenciaFlujo = asistenciaFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(AsistenciaRequest asistencia)
        {
            try
            {
                var resultado = await _asistenciaFlujo.Registrar(asistencia);
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
            var resultado = await _asistenciaFlujo.Obtener(idTrabajador);

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("resumen")]
        public async Task<IActionResult> ObtenerResumen(Guid idTrabajador, DateOnly fechaInicio, DateOnly fechaFin)
        {
            var resultado = await _asistenciaFlujo.ObtenerResumen(idTrabajador, fechaInicio, fechaFin);
            return Ok(resultado);
        }
    }
}
