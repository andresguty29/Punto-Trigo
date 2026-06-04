using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Proveedor.Proveedor;

namespace Abstracciones.Interfaces.API.ProveedorAPI
{
    public interface IProveedorController
    {
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> Agregar(ProveedorRequest proveedor);
        Task<IActionResult> Editar(Guid Id, ProveedorRequest proveedor);
        Task<IActionResult> Eliminar(Guid Id);
    }
}
