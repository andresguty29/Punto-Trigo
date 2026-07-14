using static Abstracciones.Modelos.Reporte.Reporte;

namespace Abstracciones.Interfaces.DA.ReporteDA
{
    public interface IReporteDA
    {
        Task<IEnumerable<ReporteDiaResponse>> Obtener(DateOnly fechaInicio, DateOnly fechaFin);
    }
}
