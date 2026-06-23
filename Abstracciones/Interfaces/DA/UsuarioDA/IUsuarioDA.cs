using static Abstracciones.Modelos.Usuario.Usuario;

namespace Abstracciones.Interfaces.DA.UsuarioDA
{
    public interface IUsuarioDA
    {
        Task<IEnumerable<UsuarioResponse>> Obtener();
        Task<UsuarioResponse?> Obtener(Guid Id);
        Task<UsuarioResponse?> ObtenerPorNombre(string nombreUsuario);
        Task<Guid> Agregar(UsuarioRequest usuario);
        Task<Guid> Editar(Guid Id, UsuarioRequest usuario);
        Task<Guid> Eliminar(Guid Id);
        Task<Guid> Activar(Guid Id);
        Task<Guid> CambiarContrasena(Guid Id, string contrasenaHash);
    }
}
