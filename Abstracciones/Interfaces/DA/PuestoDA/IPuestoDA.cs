using static Abstracciones.Modelos.Puesto.Puesto;

namespace Abstracciones.Interfaces.DA.PuestoDA
{
    public interface IPuestoDA
    {
        Task<IEnumerable<PuestoResponse>> Obtener();
        Task<PuestoResponse> Obtener(Guid Id);
        Task<Guid> Agregar(PuestoRequest puesto);
        Task<Guid> Editar(Guid Id, PuestoRequest puesto);
        Task<Guid> Eliminar(Guid Id);
        Task<Guid> Activar(Guid Id);
    }
}
