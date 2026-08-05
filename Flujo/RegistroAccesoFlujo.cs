using Abstracciones.Interfaces.DA.RegistroAccesoDA;
using Abstracciones.Interfaces.Flujo.RegistroAcceso;
using static Abstracciones.Modelos.RegistroAcceso.RegistroAcceso;

namespace Flujo
{
    public class RegistroAccesoFlujo : IRegistroAccesoFlujo
    {
        private readonly IRegistroAccesoDA _registroAccesoDA;

        public RegistroAccesoFlujo(IRegistroAccesoDA registroAccesoDA)
        {
            _registroAccesoDA = registroAccesoDA;
        }

        public Task<Guid> Registrar(RegistrarAccesoRequest registro)
        {
            return _registroAccesoDA.Registrar(registro);
        }

        public Task CerrarSesion(Guid idRegistro)
        {
            return _registroAccesoDA.CerrarSesion(idRegistro);
        }

        public Task<IEnumerable<RegistroAccesoResponse>> Obtener(DateOnly? fechaInicio, DateOnly? fechaFin, string? nombreUsuario)
        {
            return _registroAccesoDA.Obtener(fechaInicio, fechaFin, nombreUsuario);
        }

        public Task<RegistroAccesoResponse?> Obtener(Guid id)
        {
            return _registroAccesoDA.Obtener(id);
        }
    }
}
