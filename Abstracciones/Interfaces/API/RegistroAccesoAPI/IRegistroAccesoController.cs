using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.RegistroAcceso.RegistroAcceso;

namespace Abstracciones.Interfaces.API.RegistroAccesoAPI
{
    public interface IRegistroAccesoController
    {
        Task<IActionResult> Registrar(RegistrarAccesoRequest registro);
        Task<IActionResult> CerrarSesion(Guid Id);
        Task<IActionResult> Obtener(DateOnly? fechaInicio, DateOnly? fechaFin, string? nombreUsuario);
        Task<IActionResult> Obtener(Guid Id);
    }
}
