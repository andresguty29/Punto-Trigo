using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.PlanillaDA;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Planilla.Planilla;

namespace DA.PlanillaDA
{
    public class PlanillaDA : IPlanillaDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public PlanillaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> GenerarDetallePago(GenerarDetallePagoRequest request)
        {
            return await _sqlConnection.ExecuteScalarAsync<Guid>("Generar_Detalle_Pago", new
            {
                request.Id_Trabajador,
                request.Periodo,
                request.Fecha_Inicio,
                request.Fecha_Fin
            });
        }

        public async Task<DetallePagoResponse?> ObtenerDetalle(Guid idPlanilla)
        {
            var resultado = await _sqlConnection.QueryAsync<DetallePagoResponse>("Obtener_Detalle_Pago", new
            {
                Id_Planilla = idPlanilla
            });

            return resultado.FirstOrDefault();
        }

        public async Task<IEnumerable<DetallePagoResponse>> ObtenerHistorial(DateOnly? fechaInicio, DateOnly? fechaFin)
        {
            return await _sqlConnection.QueryAsync<DetallePagoResponse>("Obtener_Historial_Planillas", new
            {
                Fecha_Inicio = fechaInicio,
                Fecha_Fin = fechaFin
            });
        }
    }
}
