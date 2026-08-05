using static Abstracciones.Modelos.RegistroAcceso.RegistroAcceso;

namespace Abstracciones.Interfaces.DA.RegistroAccesoDA
{
    public interface IRegistroAccesoDA
    {
        Task<Guid> Registrar(RegistrarAccesoRequest registro);
        Task CerrarSesion(Guid idRegistro);
        Task<IEnumerable<RegistroAccesoResponse>> Obtener(DateOnly? fechaInicio, DateOnly? fechaFin, string? nombreUsuario);
        Task<RegistroAccesoResponse?> Obtener(Guid id);
    }
}
