using Abstracciones.Interfaces.DA.ProduccionDA;
using Abstracciones.Interfaces.Flujo.Produccion;
using static Abstracciones.Modelos.Produccion.Produccion;

namespace Flujo
{
    public class ProduccionFlujo : IProduccionFlujo
    {
        private readonly IProduccionDA _produccionDA;

        public ProduccionFlujo(IProduccionDA produccionDA)
        {
            _produccionDA = produccionDA;
        }

        public Task<IEnumerable<AsignacionResponse>> ObtenerAsignaciones()
        {
            return _produccionDA.ObtenerAsignaciones();
        }

        public Task<AsignacionResponse> ObtenerAsignacion(Guid Id_Asignacion)
        {
            return _produccionDA.ObtenerAsignacion(Id_Asignacion);
        }

        public Task<Guid> AgregarAsignacion(AsignacionRequest asignacion)
        {
            return _produccionDA.AgregarAsignacion(asignacion);
        }

        public Task<Guid> EditarAsignacion(Guid Id_Asignacion, AsignacionRequest asignacion)
        {
            return _produccionDA.EditarAsignacion(Id_Asignacion, asignacion);
        }

        public Task<Guid> EliminarAsignacion(Guid Id_Asignacion)
        {
            return _produccionDA.EliminarAsignacion(Id_Asignacion);
        }

        public Task<Guid> ActivarAsignacion(Guid Id_Asignacion)
        {
            return _produccionDA.ActivarAsignacion(Id_Asignacion);
        }

        public Task<IEnumerable<ListaProduccionResponse>> ObtenerListaDiaria(Guid? Id_Trabajador)
        {
            return _produccionDA.ObtenerListaDiaria(Id_Trabajador);
        }

        public Task<IEnumerable<ProductoAsignadoResponse>> ObtenerProductosAsignados(Guid Id_Trabajador)
        {
            return _produccionDA.ObtenerProductosAsignados(Id_Trabajador);
        }
    }
}
