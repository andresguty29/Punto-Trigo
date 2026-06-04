using System.Net.Http.Json;
using static Abstracciones.Modelos.Trabajador.Trabajador;

namespace Web.Services
{
    public class TrabajadorService
    {
        private readonly HttpClient _httpClient;

        public TrabajadorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<TrabajadorResponse>> Obtener()
        {
            var resultado = await _httpClient.GetFromJsonAsync<IEnumerable<TrabajadorResponse>>("api/Trabajador");
            return resultado ?? new List<TrabajadorResponse>();
        }

        public async Task<TrabajadorResponse?> Obtener(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<TrabajadorResponse>($"api/Trabajador/{id}");
        }

        public async Task<bool> Agregar(TrabajadorRequest trabajador)
        {
            var respuesta = await _httpClient.PostAsJsonAsync("api/Trabajador", trabajador);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> Editar(Guid id, TrabajadorRequest trabajador)
        {
            var respuesta = await _httpClient.PutAsJsonAsync($"api/Trabajador/{id}", trabajador);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> Eliminar(Guid id)
        {
            var respuesta = await _httpClient.DeleteAsync($"api/Trabajador/{id}");
            return respuesta.IsSuccessStatusCode;
        }
    }
}