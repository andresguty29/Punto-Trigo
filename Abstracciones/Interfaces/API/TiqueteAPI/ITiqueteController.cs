using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Tiquete.Tiquete;

namespace Abstracciones.Interfaces.API.TiqueteAPI
{
    public interface ITiqueteController
    {
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> ObtenerDetalle(Guid Id);
        Task<IActionResult> Agregar(TiqueteRequest tiquete, Guid? idTrabajador);
        Task<IActionResult> ReintentarEnvio(Guid Id);
    }
}
