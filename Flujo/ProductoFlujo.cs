using Abstracciones.Interfaces.DA.ProductoDA;
using Abstracciones.Interfaces.Flujo.Producto;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Producto.Producto;

namespace Flujo
{
    public class ProductoFlujo : IProductoFlujo
    {
        private IProductoDA _productoDA;
        public ProductoFlujo(IProductoDA productoDA)
        {
            _productoDA = productoDA;
        }
        public async Task<Guid> Agregar(ProductoRequest producto)
        {
            try
            {
                return await _productoDA.Agregar(producto);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new InvalidOperationException("Ya existe un producto con ese código.");
            }
        }

        public async Task<Guid> Editar(Guid Id, ProductoRequest producto)
        {
            try
            {
                return await _productoDA.Editar(Id, producto);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new InvalidOperationException("Ya existe un producto con ese código.");
            }
        }

        public Task<Guid> Eliminar(Guid Id)
        {
            return _productoDA.Eliminar(Id);
        }

        public Task<IEnumerable<ProductoResponse>> Obtener()
        {
            return _productoDA.Obtener();
        }

        public Task<ProductoResponse> Obtener(Guid Id)
        {
            return _productoDA.Obtener(Id);
        }

        public Task<Guid> Activar(Guid Id)
        {
            return _productoDA.Activar(Id);
        }
    }
}
