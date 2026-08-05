using static Abstracciones.Modelos.Prestamo.Prestamo;

namespace Abstracciones.Interfaces.DA.PrestamoDA
{
    public interface IPrestamoDA
    {
        Task<Guid> Registrar(PrestamoRequest prestamo);
        Task<IEnumerable<PrestamoResponse>> Obtener(Guid idTrabajador);
    }
}
