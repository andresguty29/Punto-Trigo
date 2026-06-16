using Abstracciones.Interfaces.DA.ProductoDA;
using Abstracciones.Interfaces.Flujo.Producto;
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
        public Task<Guid> Agregar(ProductoRequest producto)
        {
            return _productoDA.Agregar(producto);
        }

        public Task<Guid> Editar(Guid Id, ProductoRequest producto)
        {
            return _productoDA.Editar(Id, producto);
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
