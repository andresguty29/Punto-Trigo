using static Abstracciones.Modelos.Planilla.Planilla;

namespace Abstracciones.Interfaces.Flujo.Planilla
{
    public interface IPlanillaFlujo
    {
        Task<Guid> GenerarDetallePago(GenerarDetallePagoRequest request);
        Task<DetallePagoResponse?> ObtenerDetalle(Guid idPlanilla);
        Task<IEnumerable<DetallePagoResponse>> ObtenerHistorial(DateOnly? fechaInicio, DateOnly? fechaFin);
    }
}
