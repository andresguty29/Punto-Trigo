using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.HorasExtraDA;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.HorasExtra.HorasExtra;

namespace DA.HorasExtraDA
{
    public class HorasExtraDA : IHorasExtraDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public HorasExtraDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> Registrar(HorasExtraRequest horasExtra)
        {
            var idHorasExtra = Guid.NewGuid();

            await _sqlConnection.ExecuteAsync("Registrar_Horas_Extra", new
            {
                Id_HorasExtra = idHorasExtra,
                horasExtra.Id_Trabajador,
                horasExtra.Fecha,
                horasExtra.Horas
            });

            return idHorasExtra;
        }

        public async Task<IEnumerable<HorasExtraResponse>> Obtener(Guid idTrabajador)
        {
            return await _sqlConnection.QueryAsync<HorasExtraResponse>("Obtener_Horas_Extra_Trabajador", new
            {
                Id_Trabajador = idTrabajador
            });
        }
    }
}
