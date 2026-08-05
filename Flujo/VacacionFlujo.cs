using Abstracciones.Interfaces.DA.VacacionDA;
using Abstracciones.Interfaces.Flujo.Vacacion;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Vacacion.Vacacion;

namespace Flujo
{
    public class VacacionFlujo : IVacacionFlujo
    {
        private readonly IVacacionDA _vacacionDA;

        public VacacionFlujo(IVacacionDA vacacionDA)
        {
            _vacacionDA = vacacionDA;
        }

        public async Task<IEnumerable<VacacionAsignadaResponse>> Asignar(Guid idTrabajador)
        {
            try
            {
                return await _vacacionDA.Asignar(idTrabajador);
            }
            catch (SqlException ex) when (ex.Number == 50000)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Task<IEnumerable<VacacionAsignadaResponse>> Obtener(Guid idTrabajador)
        {
            return _vacacionDA.Obtener(idTrabajador);
        }
    }
}
