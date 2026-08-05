using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Asistencia.Asistencia;

namespace Abstracciones.Interfaces.API.AsistenciaAPI
{
    public interface IAsistenciaController
    {
        Task<IActionResult> Registrar(AsistenciaRequest asistencia);
        Task<IActionResult> Obtener(Guid idTrabajador);
        Task<IActionResult> ObtenerResumen(Guid idTrabajador, DateOnly fechaInicio, DateOnly fechaFin);
    }
}
