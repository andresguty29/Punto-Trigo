using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Puesto.Puesto;

namespace Abstracciones.Interfaces.API.PuestoAPI
{
    public interface IPuestoController
    {
       
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> Agregar(PuestoRequest puesto);
        Task<IActionResult> Editar(Guid Id, PuestoRequest puesto);
        Task<IActionResult> Eliminar(Guid Id);
    }
}

