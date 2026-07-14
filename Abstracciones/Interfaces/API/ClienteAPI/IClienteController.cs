using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Cliente.Cliente;

namespace Abstracciones.Interfaces.API.ClienteAPI
{
    public interface IClienteController
    {
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> ObtenerPorCedula(string cedula);
        Task<IActionResult> Agregar(ClienteRequest cliente);
        Task<IActionResult> Editar(Guid Id, ClienteRequest cliente);
        Task<IActionResult> Eliminar(Guid Id);
    }
}
