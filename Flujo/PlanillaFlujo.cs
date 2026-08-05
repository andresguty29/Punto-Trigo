using Abstracciones.Interfaces.DA.PlanillaDA;
using Abstracciones.Interfaces.Flujo.Planilla;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Planilla.Planilla;

namespace Flujo
{
    public class PlanillaFlujo : IPlanillaFlujo
    {
        private readonly IPlanillaDA _planillaDA;

        public PlanillaFlujo(IPlanillaDA planillaDA)
        {
            _planillaDA = planillaDA;
        }

        public async Task<Guid> GenerarDetallePago(GenerarDetallePagoRequest request)
        {
            if (request.Fecha_Fin < request.Fecha_Inicio)
                throw new InvalidOperationException("La fecha final no puede ser menor a la fecha inicial.");

            try
            {
                return await _planillaDA.GenerarDetallePago(request);
            }
            catch (SqlException ex) when (ex.Number == 50000)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Task<DetallePagoResponse?> ObtenerDetalle(Guid idPlanilla)
        {
            return _planillaDA.ObtenerDetalle(idPlanilla);
        }

        public Task<IEnumerable<DetallePagoResponse>> ObtenerHistorial(DateOnly? fechaInicio, DateOnly? fechaFin)
        {
            return _planillaDA.ObtenerHistorial(fechaInicio, fechaFin);
        }
    }
}
