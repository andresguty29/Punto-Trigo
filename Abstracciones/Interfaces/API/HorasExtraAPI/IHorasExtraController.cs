using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.HorasExtra.HorasExtra;

namespace Abstracciones.Interfaces.API.HorasExtraAPI
{
    public interface IHorasExtraController
    {
        Task<IActionResult> Registrar(HorasExtraRequest horasExtra);
        Task<IActionResult> Obtener(Guid idTrabajador);
    }
}
