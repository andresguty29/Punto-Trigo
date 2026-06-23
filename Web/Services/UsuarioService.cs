using System.Net.Http.Json;
using static Abstracciones.Modelos.Usuario.Usuario;

namespace Web.Services
{
    public class UsuarioService(HttpClient httpClient)
        : CrudService<UsuarioResponse, UsuarioRequest>(httpClient)
    {
        protected override string Ruta => "Usuario";

        public async Task<LoginResponse?> Login(LoginRequest request)
        {
            var respuesta = await _httpClient.PostAsJsonAsync("api/Usuario/login", request);
            if (!respuesta.IsSuccessStatusCode) return null;
            return await respuesta.Content.ReadFromJsonAsync<LoginResponse>();
        }

        public async Task<bool> CambiarContrasena(Guid id, CambiarContrasenaRequest request)
        {
            var respuesta = await _httpClient.PostAsJsonAsync($"api/Usuario/{id}/cambiar-contrasena", request);
            return respuesta.IsSuccessStatusCode;
        }
    }
}
