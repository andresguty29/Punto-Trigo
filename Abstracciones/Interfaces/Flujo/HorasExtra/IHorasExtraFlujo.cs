using static Abstracciones.Modelos.HorasExtra.HorasExtra;

namespace Abstracciones.Interfaces.Flujo.HorasExtra
{
    public interface IHorasExtraFlujo
    {
        Task<Guid> Registrar(HorasExtraRequest horasExtra);
        Task<IEnumerable<HorasExtraResponse>> Obtener(Guid idTrabajador);
    }
}
