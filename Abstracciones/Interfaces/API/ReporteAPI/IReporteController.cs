using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API.ReporteAPI
{
    public interface IReporteController
    {
        Task<IActionResult> Obtener(DateOnly fechaInicio, DateOnly fechaFin);
    }
}
