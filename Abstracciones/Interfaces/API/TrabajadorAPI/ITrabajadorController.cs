using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Trabajador.Trabajador;

namespace Abstracciones.Interfaces.API.TrabajadorAPI
{
    public interface ITrabajadorController
    {
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> Agregar(TrabajadorRequest trabajador);
        Task<IActionResult> Editar(Guid Id, TrabajadorRequest trabajador);
        Task<IActionResult> Eliminar(Guid Id);
        Task<IActionResult> ObtenerPanaderos();

    }
}
