using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Produccion.Produccion;

namespace Abstracciones.Interfaces.API.ProduccionAPI
{
    public interface IProduccionController
    {
        Task<IActionResult> ObtenerAsignaciones();
        Task<IActionResult> ObtenerAsignacion(Guid Id);
        Task<IActionResult> AgregarAsignacion(AsignacionRequest asignacion);
        Task<IActionResult> EditarAsignacion(Guid Id, AsignacionRequest asignacion);
        Task<IActionResult> EliminarAsignacion(Guid Id);
        Task<IActionResult> ActivarAsignacion(Guid Id);
        Task<IActionResult> ObtenerListaDiaria(Guid? Id_Trabajador);
        Task<IActionResult> ObtenerProductosAsignados(Guid Id_Trabajador);
        Task<IActionResult> ObtenerMateriales(Guid Id_Asignacion);
        Task<IActionResult> MarcarRealizada(Guid Id_Asignacion);
    }
}
