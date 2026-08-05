using Abstracciones.Interfaces.DA.PrestamoDA;
using Abstracciones.Interfaces.Flujo.Prestamo;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Prestamo.Prestamo;

namespace Flujo
{
    public class PrestamoFlujo : IPrestamoFlujo
    {
        private readonly IPrestamoDA _prestamoDA;

        public PrestamoFlujo(IPrestamoDA prestamoDA)
        {
            _prestamoDA = prestamoDA;
        }

        public async Task<Guid> Registrar(PrestamoRequest prestamo)
        {
            try
            {
                return await _prestamoDA.Registrar(prestamo);
            }
            catch (SqlException ex) when (ex.Number == 50000)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Task<IEnumerable<PrestamoResponse>> Obtener(Guid idTrabajador)
        {
            return _prestamoDA.Obtener(idTrabajador);
        }
    }
}
