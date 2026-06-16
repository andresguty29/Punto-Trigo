using System.Net;
using System.Net.Http.Json;

namespace Web.Services
{
    public abstract class CrudService<TResponse, TRequest>
    {
        protected readonly HttpClient _httpClient;
        protected abstract string Ruta { get; }

        protected CrudService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<TResponse>> Obtener()
        {
            var response = await _httpClient.GetAsync($"api/{Ruta}");

            if (response.StatusCode == HttpStatusCode.NoContent)
                return [];

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<TResponse>>() ?? [];
        }

        public async Task<TResponse?> Obtener(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<TResponse>($"api/{Ruta}/{id}");
        }

        public async Task<bool> Agregar(TRequest entidad)
        {
            var respuesta = await _httpClient.PostAsJsonAsync($"api/{Ruta}", entidad);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> Editar(Guid id, TRequest entidad)
        {
            var respuesta = await _httpClient.PutAsJsonAsync($"api/{Ruta}/{id}", entidad);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> Eliminar(Guid id)
        {
            var respuesta = await _httpClient.DeleteAsync($"api/{Ruta}/{id}");
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> Activar(Guid id)
        {
            var respuesta = await _httpClient.PatchAsync($"api/{Ruta}/{id}/activar", null);
            return respuesta.IsSuccessStatusCode;
        }
    }
}
