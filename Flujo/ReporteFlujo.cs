using Abstracciones.Interfaces.DA.ReporteDA;
using Abstracciones.Interfaces.Flujo.Reporte;
using static Abstracciones.Modelos.Reporte.Reporte;

namespace Flujo
{
    public class ReporteFlujo : IReporteFlujo
    {
        private readonly IReporteDA _reporteDA;

        public ReporteFlujo(IReporteDA reporteDA)
        {
            _reporteDA = reporteDA;
        }

        public Task<IEnumerable<ReporteDiaResponse>> Obtener(DateOnly fechaInicio, DateOnly fechaFin)
        {
            var hoy = DateOnly.FromDateTime(DateTime.Today);

            if (fechaFin < fechaInicio)
                throw new InvalidOperationException("La fecha final no puede ser menor a la fecha inicial.");

            if (fechaInicio > hoy || fechaFin > hoy)
                throw new InvalidOperationException("No se puede generar un reporte con fechas futuras.");

            return _reporteDA.Obtener(fechaInicio, fechaFin);
        }
    }
}
