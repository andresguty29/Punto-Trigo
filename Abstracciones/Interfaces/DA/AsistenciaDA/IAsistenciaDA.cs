using static Abstracciones.Modelos.Asistencia.Asistencia;

namespace Abstracciones.Interfaces.DA.AsistenciaDA
{
    public interface IAsistenciaDA
    {
        Task<Guid> Registrar(AsistenciaRequest asistencia);
        Task<IEnumerable<AsistenciaResponse>> Obtener(Guid idTrabajador);
        Task<ResumenAsistenciaResponse> ObtenerResumen(Guid idTrabajador, DateOnly fechaInicio, DateOnly fechaFin);
    }
}
