using static Abstracciones.Modelos.HorasExtra.HorasExtra;

namespace Abstracciones.Interfaces.DA.HorasExtraDA
{
    public interface IHorasExtraDA
    {
        Task<Guid> Registrar(HorasExtraRequest horasExtra);
        Task<IEnumerable<HorasExtraResponse>> Obtener(Guid idTrabajador);
    }
}
