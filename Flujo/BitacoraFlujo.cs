using Abstracciones.Interfaces.DA.BitacoraDA;
using Abstracciones.Interfaces.Flujo.Bitacora;
using static Abstracciones.Modelos.Bitacora.Bitacora;

namespace Flujo
{
    public class BitacoraFlujo : IBitacoraFlujo
    {
        private readonly IBitacoraDA _bitacoraDA;

        public BitacoraFlujo(IBitacoraDA bitacoraDA)
        {
            _bitacoraDA = bitacoraDA;
        }

        public Task<Guid> Registrar(RegistrarBitacoraRequest registro)
        {
            return _bitacoraDA.Registrar(registro);
        }

        public Task<IEnumerable<BitacoraResponse>> Obtener(DateOnly? fechaInicio, DateOnly? fechaFin)
        {
            return _bitacoraDA.Obtener(fechaInicio, fechaFin);
        }
    }
}
