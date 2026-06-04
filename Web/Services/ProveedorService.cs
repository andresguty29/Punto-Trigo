using System.Net.Http.Json;
using static Abstracciones.Modelos.Proveedor.Proveedor;

namespace Web.Services
{
    public class ProveedorService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://localhost:44378/api/Proveedor";

        public ProveedorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProveedorResponse>> Obtener()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ProveedorResponse>>(ApiUrl)
          ?? new List<ProveedorResponse>();
        }

        public async Task<ProveedorResponse?> Obtener(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<ProveedorResponse>($"api/Proveedor/{id}");
        }

        public async Task<bool> Agregar(ProveedorRequest proveedor)
        {
            var respuesta = await _httpClient.PostAsJsonAsync("api/Proveedor", proveedor);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> Editar(Guid id, ProveedorRequest proveedor)
        {
            var respuesta = await _httpClient.PutAsJsonAsync($"api/Proveedor/{id}", proveedor);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> Eliminar(Guid id)
        {
            var respuesta = await _httpClient.DeleteAsync($"api/Proveedor/{id}");
            return respuesta.IsSuccessStatusCode;
        }
    }
}