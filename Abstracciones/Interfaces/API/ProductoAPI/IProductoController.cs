using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Producto.Producto;

namespace Abstracciones.Interfaces.API.ProductoAPI
{
    public interface IProductoController
    {
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> Agregar(ProductoRequest producto);
        Task<IActionResult> Editar(Guid Id, ProductoRequest producto);
        Task<IActionResult> Eliminar(Guid Id);
    }
}
