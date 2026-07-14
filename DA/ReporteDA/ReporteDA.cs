using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.ReporteDA;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Reporte.Reporte;

namespace DA.ReporteDA
{
    public class ReporteDA : IReporteDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public ReporteDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<IEnumerable<ReporteDiaResponse>> Obtener(DateOnly fechaInicio, DateOnly fechaFin)
        {
            return await _sqlConnection.QueryAsync<ReporteDiaResponse>("Obtener_Reporte_Financiero", new
            {
                Fecha_Inicio = fechaInicio.ToDateTime(TimeOnly.MinValue),
                Fecha_Fin = fechaFin.ToDateTime(TimeOnly.MinValue)
            });
        }
    }
}
