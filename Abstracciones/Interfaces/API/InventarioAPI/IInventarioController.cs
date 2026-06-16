using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Inventario.Inventario;
using static Abstracciones.Modelos.Inventario.Movimiento;

namespace Abstracciones.Interfaces.API.InventarioAPI
{
    public interface IInventarioController
    {
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> Agregar(InventarioRequest inventario);
        Task<IActionResult> Editar(Guid Id, InventarioRequest inventario);
        Task<IActionResult> Eliminar(Guid Id);
        Task<IActionResult> Activar(Guid Id);
        Task<IActionResult> RegistrarMovimiento(MovimientoRequest movimiento);
        Task<IActionResult> ObtenerMovimientos(Guid Id);
    }
}
