using System.Net.Http.Json;
using static Abstracciones.Modelos.Puesto.Puesto;

namespace Web.Services
{
    public class PuestoService
    {
        private readonly HttpClient _httpClient;

        public PuestoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PuestoResponse>> Obtener()
        {
            var resultado = await _httpClient.GetFromJsonAsync<IEnumerable<PuestoResponse>>("api/Puesto");
            return resultado ?? new List<PuestoResponse>();
        }

        public async Task<PuestoResponse?> Obtener(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<PuestoResponse>($"api/Puesto/{id}");
        }

        public async Task<bool> Agregar(PuestoRequest puesto)
        {
            var respuesta = await _httpClient.PostAsJsonAsync("api/Puesto", puesto);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> Editar(Guid id, PuestoRequest puesto)
        {
            var respuesta = await _httpClient.PutAsJsonAsync($"api/Puesto/{id}", puesto);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> Eliminar(Guid id)
        {
            var respuesta = await _httpClient.DeleteAsync($"api/Puesto/{id}");
            return respuesta.IsSuccessStatusCode;
        }
    }
}