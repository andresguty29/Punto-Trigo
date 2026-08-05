using static Abstracciones.Modelos.Bitacora.Bitacora;

namespace Abstracciones.Interfaces.Flujo.Bitacora
{
    public interface IBitacoraFlujo
    {
        Task<Guid> Registrar(RegistrarBitacoraRequest registro);
        Task<IEnumerable<BitacoraResponse>> Obtener(DateOnly? fechaInicio, DateOnly? fechaFin);
    }
}
