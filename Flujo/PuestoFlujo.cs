using Abstracciones.Interfaces.DA.PuestoDA;
using Abstracciones.Interfaces.Flujo.Puesto;
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

        public Task<Guid> Agregar(PuestoRequest puesto)
        {
            return _puestoDA.Agregar(puesto);
        }

        public Task<Guid> Editar(Guid Id, PuestoRequest puesto)
        {
            return _puestoDA.Editar(Id, puesto);
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
