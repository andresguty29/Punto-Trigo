using Abstracciones.Interfaces.DA.PuestoDA;
using Abstracciones.Interfaces.Flujo.Puesto;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Puesto.Puesto;

namespace Flujo
{
    public class PuestoFlujo : IPuestoFlujo
    {
        private IPuestoDA _puestoDA;

        public PuestoFlujo(IPuestoDA puestoDA)
        {
            _puestoDA = puestoDA;
        }

        public async Task<Guid> Agregar(PuestoRequest puesto)
        {
            try
            {
                return await _puestoDA.Agregar(puesto);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new InvalidOperationException("Ya existe un puesto con ese nombre.");
            }
        }

        public async Task<Guid> Editar(Guid Id, PuestoRequest puesto)
        {
            try
            {
                return await _puestoDA.Editar(Id, puesto);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new InvalidOperationException("Ya existe un puesto con ese nombre.");
            }
        }

        public Task<Guid> Eliminar(Guid Id)
        {
            return _puestoDA.Eliminar(Id);
        }

        public Task<IEnumerable<PuestoResponse>> Obtener()
        {
            return _puestoDA.Obtener();
        }

        public Task<PuestoResponse> Obtener(Guid Id)
        {
            return _puestoDA.Obtener(Id);
        }

        public Task<Guid> Activar(Guid Id)
        {
            return _puestoDA.Activar(Id);
        }
    }
}
