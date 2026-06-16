using static Abstracciones.Modelos.Usuario.Usuario;

namespace Web.Services
{
    public class UsuarioService(HttpClient httpClient)
        : CrudService<UsuarioResponse, UsuarioRequest>(httpClient)
    {
        protected override string Ruta => "Usuario";
    }
}
