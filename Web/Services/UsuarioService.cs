using System.Net;
using System.Net.Http.Json;
using static Abstracciones.Modelos.Usuario.Usuario;

namespace Web.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _httpClient;

        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<UsuarioResponse>> Obtener()
        {
            var response = await _httpClient.GetAsync("https://localhost:44378/api/Usuario");

            if (response.StatusCode == HttpStatusCode.NoContent)
                return new List<UsuarioResponse>();

            response.EnsureSuccessStatusCode();

            var resultado = await response.Content.ReadFromJsonAsync<IEnumerable<UsuarioResponse>>();

            return resultado ?? new List<UsuarioResponse>();
        }

        public async Task<UsuarioResponse?> Obtener(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<UsuarioResponse>($"api/Usuario/{id}");
        }

        public async Task<bool> Agregar(UsuarioRequest usuario)
        {
            var respuesta = await _httpClient.PostAsJsonAsync("api/Usuario", usuario);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> Editar(Guid id, UsuarioRequest usuario)
        {
            var respuesta = await _httpClient.PutAsJsonAsync($"api/Usuario/{id}", usuario);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> Eliminar(Guid id)
        {
            var respuesta = await _httpClient.DeleteAsync($"api/Usuario/{id}");
            return respuesta.IsSuccessStatusCode;
        }
    }
}