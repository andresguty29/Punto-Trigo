using Abstracciones.Interfaces.DA.AsistenciaDA;
using Abstracciones.Interfaces.Flujo.Asistencia;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Asistencia.Asistencia;

namespace Flujo
{
    public class AsistenciaFlujo : IAsistenciaFlujo
    {
        private readonly IAsistenciaDA _asistenciaDA;

        public AsistenciaFlujo(IAsistenciaDA asistenciaDA)
        {
            _asistenciaDA = asistenciaDA;
        }

        public async Task<Guid> Registrar(AsistenciaRequest asistencia)
        {
            if (!TiposEventoValidos.Contains(asistencia.Tipo_Evento))
                throw new InvalidOperationException("El tipo de registro indicado no es válido.");

            try
            {
                return await _asistenciaDA.Registrar(asistencia);
            }
            catch (SqlException ex) when (ex.Number == 50000)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Task<IEnumerable<AsistenciaResponse>> Obtener(Guid idTrabajador)
        {
            return _asistenciaDA.Obtener(idTrabajador);
        }

        public Task<ResumenAsistenciaResponse> ObtenerResumen(Guid idTrabajador, DateOnly fechaInicio, DateOnly fechaFin)
        {
            return _asistenciaDA.ObtenerResumen(idTrabajador, fechaInicio, fechaFin);
        }
    }
}
