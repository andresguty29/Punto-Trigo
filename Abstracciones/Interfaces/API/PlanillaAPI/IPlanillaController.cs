using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Planilla.Planilla;

namespace Abstracciones.Interfaces.API.PlanillaAPI
{
    public interface IPlanillaController
    {
        Task<IActionResult> GenerarDetallePago(GenerarDetallePagoRequest request);
        Task<IActionResult> ObtenerDetalle(Guid Id);
        Task<IActionResult> ObtenerHistorial(DateOnly? fechaInicio, DateOnly? fechaFin);
    }
}
