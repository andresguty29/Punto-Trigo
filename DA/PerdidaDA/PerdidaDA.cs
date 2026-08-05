using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.PerdidaDA;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Perdida.Perdida;

namespace DA.PerdidaDA
{
    public class PerdidaDA : IPerdidaDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public PerdidaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<IEnumerable<VencimientoPendienteResponse>> ObtenerPendientes()
        {
            return await _sqlConnection.QueryAsync<VencimientoPendienteResponse>("Obtener_Vencimientos_Pendientes");
        }

        public async Task<ProcesarPerdidaResponse> Procesar(Guid idMovimiento)
        {
            var resultado = await _sqlConnection.QueryAsync<ProcesarPerdidaResponse>("Procesar_Perdida_Vencimiento", new
            {
                Id_Movimiento = idMovimiento
            });

            return resultado.First();
        }
    }
}
