using static Abstracciones.Modelos.Inventario.Inventario;
using static Abstracciones.Modelos.Inventario.Movimiento;

namespace Abstracciones.Interfaces.Flujo.Inventario
{
    public interface IInventarioFlujo
    {
        Task<IEnumerable<InventarioResponse>> Obtener();
        Task<InventarioResponse> Obtener(Guid Id);
        Task<Guid> Agregar(InventarioRequest inventario);
        Task<Guid> Editar(Guid Id, InventarioRequest inventario);
        Task<Guid> Eliminar(Guid Id);
        Task<Guid> Activar(Guid Id);
        Task<Guid> RegistrarMovimiento(MovimientoRequest movimiento);
        Task<IEnumerable<MovimientoResponse>> ObtenerMovimientos(Guid Id_Inventario);
    }
}
