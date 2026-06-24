using static Abstracciones.Modelos.Produccion.Produccion;

namespace Abstracciones.Interfaces.DA.ProduccionDA
{
    public interface IProduccionDA
    {
        Task<IEnumerable<AsignacionResponse>> ObtenerAsignaciones();
        Task<AsignacionResponse> ObtenerAsignacion(Guid Id_Asignacion);
        Task<Guid> AgregarAsignacion(AsignacionRequest asignacion);
        Task<Guid> EditarAsignacion(Guid Id_Asignacion, AsignacionRequest asignacion);
        Task<Guid> EliminarAsignacion(Guid Id_Asignacion);
        Task<Guid> ActivarAsignacion(Guid Id_Asignacion);
        Task<IEnumerable<ListaProduccionResponse>> ObtenerListaDiaria(Guid? Id_Trabajador);
        Task<IEnumerable<ProductoAsignadoResponse>> ObtenerProductosAsignados(Guid Id_Trabajador);
    }
}
