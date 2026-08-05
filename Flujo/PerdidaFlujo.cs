using Abstracciones.Interfaces.DA.PerdidaDA;
using Abstracciones.Interfaces.Flujo.Perdida;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Perdida.Perdida;

namespace Flujo
{
    public class PerdidaFlujo : IPerdidaFlujo
    {
        private readonly IPerdidaDA _perdidaDA;

        public PerdidaFlujo(IPerdidaDA perdidaDA)
        {
            _perdidaDA = perdidaDA;
        }

        public Task<IEnumerable<VencimientoPendienteResponse>> ObtenerPendientes()
        {
            return _perdidaDA.ObtenerPendientes();
        }

        public async Task<ProcesarPerdidaResponse> Procesar(Guid idMovimiento)
        {
            try
            {
                return await _perdidaDA.Procesar(idMovimiento);
            }
            catch (SqlException ex) when (ex.Number == 50000)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
