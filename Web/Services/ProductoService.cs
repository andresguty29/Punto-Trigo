using System.Net.Http.Json;
using static Abstracciones.Modelos.Producto.Producto;

namespace Web.Services
{
    public class ProductoService
    {
        private readonly HttpClient _httpClient;

        public ProductoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductoResponse>> Obtener()
        {
            var resultado = await _httpClient.GetFromJsonAsync<IEnumerable<ProductoResponse>>("api/Producto");
            return resultado ?? new List<ProductoResponse>();
        }

        public async Task<ProductoResponse?> Obtener(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<ProductoResponse>($"api/Producto/{id}");
        }

        public async Task<bool> Agregar(ProductoRequest producto)
        {
            var respuesta = await _httpClient.PostAsJsonAsync("api/Producto", producto);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> Editar(Guid id, ProductoRequest producto)
        {
            var respuesta = await _httpClient.PutAsJsonAsync($"api/Producto/{id}", producto);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> Eliminar(Guid id)
        {
            var respuesta = await _httpClient.DeleteAsync($"api/Producto/{id}");
            return respuesta.IsSuccessStatusCode;
        }
    }
}