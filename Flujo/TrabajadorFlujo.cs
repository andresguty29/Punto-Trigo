using Abstracciones.Interfaces.DA.TrabajadorDA;
using Abstracciones.Interfaces.Flujo.Trabajador;
using static Abstracciones.Modelos.Trabajador.Trabajador;

namespace Flujo
{
    public class TrabajadorFlujo : ITrabajadorFlujo
    {
        private ITrabajadorDA _trabajadorDA;

        public TrabajadorFlujo(ITrabajadorDA trabajadorDA)
        {
            _trabajadorDA = trabajadorDA;
        }

        public Task<Guid> Agregar(TrabajadorRequest trabajador)
        {
            return _trabajadorDA.Agregar(trabajador);
        }

        public Task<Guid> Editar(Guid Id, TrabajadorRequest trabajador)
        {
            return _trabajadorDA.Editar(Id, trabajador);
        }

        public Task<Guid> Eliminar(Guid Id)
        {
            return _trabajadorDA.Eliminar(Id);
        }

        public Task<IEnumerable<TrabajadorResponse>> Obtener()
        {
            return _trabajadorDA.Obtener();
        }

        public Task<TrabajadorResponse> Obtener(Guid Id)
        {
            return _trabajadorDA.Obtener(Id);
        }

        public Task<Guid> Activar(Guid Id)
        {
            return _trabajadorDA.Activar(Id);
        }
    }
}
