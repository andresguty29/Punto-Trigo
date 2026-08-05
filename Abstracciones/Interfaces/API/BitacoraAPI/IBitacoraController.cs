using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Bitacora.Bitacora;

namespace Abstracciones.Interfaces.API.BitacoraAPI
{
    public interface IBitacoraController
    {
        Task<IActionResult> Registrar(RegistrarBitacoraRequest registro);
        Task<IActionResult> Obtener(DateOnly? fechaInicio, DateOnly? fechaFin);
    }
}
