using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Compra.Compra;

namespace Abstracciones.Interfaces.API.CompraAPI
{
    public interface ICompraController
    {
        Task<IActionResult> Obtener(DateOnly? fechaInicio, DateOnly? fechaFin, decimal? montoMinimo);
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> ObtenerDetalle(Guid Id);
        Task<IActionResult> Agregar(CompraRequest compra);
        Task<IActionResult> Anular(Guid Id);
        Task<IActionResult> Reclasificar(Guid Id, ReclasificarCompraRequest reclasificacion);
    }
}
