using static Abstracciones.Modelos.Vacacion.Vacacion;

namespace Abstracciones.Interfaces.Flujo.Vacacion
{
    public interface IVacacionFlujo
    {
        Task<IEnumerable<VacacionAsignadaResponse>> Asignar(Guid idTrabajador);
        Task<IEnumerable<VacacionAsignadaResponse>> Obtener(Guid idTrabajador);
    }
}
