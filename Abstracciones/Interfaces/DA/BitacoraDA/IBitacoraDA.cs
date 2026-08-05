using static Abstracciones.Modelos.Bitacora.Bitacora;

namespace Abstracciones.Interfaces.DA.BitacoraDA
{
    public interface IBitacoraDA
    {
        Task<Guid> Registrar(RegistrarBitacoraRequest registro);
        Task<IEnumerable<BitacoraResponse>> Obtener(DateOnly? fechaInicio, DateOnly? fechaFin);
    }
}
