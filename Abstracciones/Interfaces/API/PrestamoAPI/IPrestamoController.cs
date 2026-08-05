using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Prestamo.Prestamo;

namespace Abstracciones.Interfaces.API.PrestamoAPI
{
    public interface IPrestamoController
    {
        Task<IActionResult> Registrar(PrestamoRequest prestamo);
        Task<IActionResult> Obtener(Guid idTrabajador);
    }
}
