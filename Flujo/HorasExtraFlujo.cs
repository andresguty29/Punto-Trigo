using Abstracciones.Interfaces.DA.HorasExtraDA;
using Abstracciones.Interfaces.Flujo.HorasExtra;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.HorasExtra.HorasExtra;

namespace Flujo
{
    public class HorasExtraFlujo : IHorasExtraFlujo
    {
        private readonly IHorasExtraDA _horasExtraDA;

        public HorasExtraFlujo(IHorasExtraDA horasExtraDA)
        {
            _horasExtraDA = horasExtraDA;
        }

        public async Task<Guid> Registrar(HorasExtraRequest horasExtra)
        {
            try
            {
                return await _horasExtraDA.Registrar(horasExtra);
            }
            catch (SqlException ex) when (ex.Number == 50000)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Task<IEnumerable<HorasExtraResponse>> Obtener(Guid idTrabajador)
        {
            return _horasExtraDA.Obtener(idTrabajador);
        }
    }
}
