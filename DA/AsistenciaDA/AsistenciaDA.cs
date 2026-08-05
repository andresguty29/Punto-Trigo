using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.AsistenciaDA;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Asistencia.Asistencia;

namespace DA.AsistenciaDA
{
    public class AsistenciaDA : IAsistenciaDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public AsistenciaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> Registrar(AsistenciaRequest asistencia)
        {
            var idAsistencia = Guid.NewGuid();

            await _sqlConnection.ExecuteAsync("Registrar_Asistencia", new
            {
                Id_Asistencia = idAsistencia,
                asistencia.Id_Trabajador,
                Fecha = asistencia.Fecha.ToDateTime(TimeOnly.MinValue),
                asistencia.Tipo_Evento,
                asistencia.Observaciones
            });

            return idAsistencia;
        }

        public async Task<IEnumerable<AsistenciaResponse>> Obtener(Guid idTrabajador)
        {
            return await _sqlConnection.QueryAsync<AsistenciaResponse>("Obtener_Asistencia_Trabajador", new
            {
                Id_Trabajador = idTrabajador
            });
        }

        public async Task<ResumenAsistenciaResponse> ObtenerResumen(Guid idTrabajador, DateOnly fechaInicio, DateOnly fechaFin)
        {
            var resultado = await _sqlConnection.QueryAsync<ResumenAsistenciaResponse>("Obtener_Resumen_Asistencia", new
            {
                Id_Trabajador = idTrabajador,
                Fecha_Inicio = fechaInicio.ToDateTime(TimeOnly.MinValue),
                Fecha_Fin = fechaFin.ToDateTime(TimeOnly.MinValue)
            });

            return resultado.First();
        }
    }
}
