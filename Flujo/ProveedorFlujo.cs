using Abstracciones.Interfaces.DA.ProveedorDA;
using Abstracciones.Interfaces.Flujo.Proveedor;
using Abstracciones.Modelos.Proveedor;

namespace Flujo
{
    public class ProveedorFlujo : IProveedorFlujo
    {
        private IProveedorDA _proveedorDA;
    
        public ProveedorFlujo(IProveedorDA proveedorDA)
        {
            _proveedorDA = proveedorDA;
        }

        public Task<Guid> Agregar(Proveedor.ProveedorRequest proveedor)
        {
            return _proveedorDA.Agregar(proveedor);
        }

        public Task<Guid> Editar(Guid Id, Proveedor.ProveedorRequest proveedor)
        {
            return _proveedorDA.Editar(Id, proveedor);
        }

        public Task<Guid> Eliminar(Guid Id)
        {
            return _proveedorDA.Eliminar(Id);
        }

        public Task<IEnumerable<Proveedor.ProveedorResponse>> Obtener()
        {
            return _proveedorDA.Obtener();
        }

        public Task<Proveedor.ProveedorResponse> Obtener(Guid Id)
        {
            return _proveedorDA.Obtener(Id);
        }
    }
}
