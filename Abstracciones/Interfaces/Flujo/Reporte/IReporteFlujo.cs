using static Abstracciones.Modelos.Reporte.Reporte;

namespace Abstracciones.Interfaces.Flujo.Reporte
{
    public interface IReporteFlujo
    {
        Task<IEnumerable<ReporteDiaResponse>> Obtener(DateOnly fechaInicio, DateOnly fechaFin);
    }
}
