using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API.VacacionAPI
{
    public interface IVacacionController
    {
        Task<IActionResult> Asignar(Guid idTrabajador);
        Task<IActionResult> Obtener(Guid idTrabajador);
    }
}
