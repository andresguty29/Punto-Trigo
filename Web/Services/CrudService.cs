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

        public async Task<(bool ok, string? error)> Agregar(TRequest entidad)
        {
            var respuesta = await _httpClient.PostAsJsonAsync($"api/{Ruta}", entidad);
            if (respuesta.IsSuccessStatusCode) return (true, null);
            var error = await LeerMensajeError(respuesta);
            return (false, error);
        }

        public async Task<(bool ok, string? error)> Editar(Guid id, TRequest entidad)
        {
            var respuesta = await _httpClient.PutAsJsonAsync($"api/{Ruta}/{id}", entidad);
            if (respuesta.IsSuccessStatusCode) return (true, null);
            var error = await LeerMensajeError(respuesta);
            return (false, error);
        }

        private static async Task<string?> LeerMensajeError(HttpResponseMessage respuesta)
        {
            try
            {
                var body = await respuesta.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                return body?.GetValueOrDefault("mensaje");
            }
            catch { return null; }
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
