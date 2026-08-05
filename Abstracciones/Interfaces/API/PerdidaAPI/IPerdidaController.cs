using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API.PerdidaAPI
{
    public interface IPerdidaController
    {
        Task<IActionResult> ObtenerPendientes();
        Task<IActionResult> Procesar(Guid idMovimiento);
    }
}
