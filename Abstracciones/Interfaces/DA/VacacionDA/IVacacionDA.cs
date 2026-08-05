using static Abstracciones.Modelos.Vacacion.Vacacion;

namespace Abstracciones.Interfaces.DA.VacacionDA
{
    public interface IVacacionDA
    {
        Task<IEnumerable<VacacionAsignadaResponse>> Asignar(Guid idTrabajador);
        Task<IEnumerable<VacacionAsignadaResponse>> Obtener(Guid idTrabajador);
    }
}
