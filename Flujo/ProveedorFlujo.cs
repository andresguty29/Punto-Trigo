using Abstracciones.Interfaces.DA.ProveedorDA;
using Abstracciones.Interfaces.Flujo.Proveedor;
using Abstracciones.Modelos.Proveedor;
using Microsoft.Data.SqlClient;

namespace Flujo
{
    public class ProveedorFlujo : IProveedorFlujo
    {
        private IProveedorDA _proveedorDA;

        public ProveedorFlujo(IProveedorDA proveedorDA)
        {
            _proveedorDA = proveedorDA;
        }

        public async Task<Guid> Agregar(Proveedor.ProveedorRequest proveedor)
        {
            try
            {
                return await _proveedorDA.Agregar(proveedor);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new InvalidOperationException("Ya existe un proveedor con esa identificación.");
            }
        }

        public async Task<Guid> Editar(Guid Id, Proveedor.ProveedorRequest proveedor)
        {
            try
            {
                return await _proveedorDA.Editar(Id, proveedor);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new InvalidOperationException("Ya existe un proveedor con esa identificación.");
            }
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

        public Task<Guid> Activar(Guid Id)
        {
            return _proveedorDA.Activar(Id);
        }
    }
}
