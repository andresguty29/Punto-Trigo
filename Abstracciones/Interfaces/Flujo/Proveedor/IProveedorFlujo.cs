using static Abstracciones.Modelos.Proveedor.Proveedor;

namespace Abstracciones.Interfaces.Flujo.Proveedor
{
    public interface IProveedorFlujo
    {
        Task<IEnumerable<ProveedorResponse>> Obtener();
        Task<ProveedorResponse> Obtener(Guid Id);
        Task<Guid> Agregar(ProveedorRequest proveedor);
        Task<Guid> Editar(Guid Id, ProveedorRequest proveedor);
        Task<Guid> Eliminar(Guid Id);
        Task<Guid> Activar(Guid Id);
    }
}
