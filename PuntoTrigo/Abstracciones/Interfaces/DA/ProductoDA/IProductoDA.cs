using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Producto.Producto;

namespace Abstracciones.Interfaces.DA.ProductoDA
{
    public interface IProductoDA
    {
        Task<IEnumerable<ProductoResponse>> Obtener();
        Task<ProductoResponse> Obtener(Guid Id);
        Task<Guid> Agregar(ProductoRequest producto);
        Task<Guid> Editar(Guid Id, ProductoRequest producto);
        Task<Guid> Eliminar(Guid Id);
    }
}
