using static Abstracciones.Modelos.Prestamo.Prestamo;

namespace Abstracciones.Interfaces.Flujo.Prestamo
{
    public interface IPrestamoFlujo
    {
        Task<Guid> Registrar(PrestamoRequest prestamo);
        Task<IEnumerable<PrestamoResponse>> Obtener(Guid idTrabajador);
    }
}
