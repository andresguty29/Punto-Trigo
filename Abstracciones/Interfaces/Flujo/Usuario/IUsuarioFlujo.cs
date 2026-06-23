using static Abstracciones.Modelos.Usuario.Usuario;

namespace Abstracciones.Interfaces.Flujo.Usuario
{
    public interface IUsuarioFlujo
    {
        Task<IEnumerable<UsuarioResponse>> Obtener();
        Task<UsuarioResponse?> Obtener(Guid Id);
        Task<Guid> Agregar(UsuarioRequest usuario);
        Task<Guid> Editar(Guid Id, UsuarioRequest usuario);
        Task<Guid> Eliminar(Guid Id);
        Task<Guid> Activar(Guid Id);
        Task<LoginResponse?> Login(LoginRequest request);
        Task<bool> CambiarContrasena(Guid Id, CambiarContrasenaRequest request);
    }
}
