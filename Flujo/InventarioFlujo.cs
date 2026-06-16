using Abstracciones.Interfaces.DA.InventarioDA;
using Abstracciones.Interfaces.Flujo.Inventario;
using static Abstracciones.Modelos.Inventario.Inventario;
using static Abstracciones.Modelos.Inventario.Movimiento;

namespace Flujo
{
    public class InventarioFlujo : IInventarioFlujo
    {
        private readonly IInventarioDA _inventarioDA;

        public InventarioFlujo(IInventarioDA inventarioDA)
        {
            _inventarioDA = inventarioDA;
        }

        public Task<IEnumerable<InventarioResponse>> Obtener() =>
            _inventarioDA.Obtener();

        public Task<InventarioResponse> Obtener(Guid Id) =>
            _inventarioDA.Obtener(Id);

        public Task<Guid> Agregar(InventarioRequest inventario) =>
            _inventarioDA.Agregar(inventario);

        public Task<Guid> Editar(Guid Id, InventarioRequest inventario) =>
            _inventarioDA.Editar(Id, inventario);

        public Task<Guid> Eliminar(Guid Id) =>
            _inventarioDA.Eliminar(Id);

        public Task<Guid> Activar(Guid Id) =>
            _inventarioDA.Activar(Id);

        public Task<Guid> RegistrarMovimiento(MovimientoRequest movimiento) =>
            _inventarioDA.RegistrarMovimiento(movimiento);

        public Task<IEnumerable<MovimientoResponse>> ObtenerMovimientos(Guid Id_Inventario) =>
            _inventarioDA.ObtenerMovimientos(Id_Inventario);
    }
}
