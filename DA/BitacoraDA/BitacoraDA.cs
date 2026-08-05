using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.BitacoraDA;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Bitacora.Bitacora;

namespace DA.BitacoraDA
{
    public class BitacoraDA : IBitacoraDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public BitacoraDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> Registrar(RegistrarBitacoraRequest registro)
        {
            var idBitacora = Guid.NewGuid();

            await _sqlConnection.ExecuteAsync("Registrar_Bitacora", new
            {
                Id_Bitacora = idBitacora,
                registro.Id_Usuario,
                registro.Nombre_Usuario,
                registro.Accion,
                registro.Detalle
            });

            return idBitacora;
        }

        public async Task<IEnumerable<BitacoraResponse>> Obtener(DateOnly? fechaInicio, DateOnly? fechaFin)
        {
            return await _sqlConnection.QueryAsync<BitacoraResponse>("Obtener_Bitacora", new
            {
                Fecha_Inicio = fechaInicio.HasValue ? fechaInicio.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null,
                Fecha_Fin = fechaFin.HasValue ? fechaFin.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null
            });
        }
    }
}
