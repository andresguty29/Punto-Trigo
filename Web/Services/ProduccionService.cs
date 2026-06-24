using System.Net;
using System.Net.Http.Json;
using static Abstracciones.Modelos.Produccion.Produccion;

namespace Web.Services
{
    public class ProduccionService
    {
        private readonly HttpClient _httpClient;

        public ProduccionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<AsignacionResponse>> ObtenerAsignaciones()
        {
            var response = await _httpClient.GetAsync("api/Produccion/asignaciones");
            if (response.StatusCode == HttpStatusCode.NoContent) return [];
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<AsignacionResponse>>() ?? [];
        }

        public async Task<AsignacionResponse?> ObtenerAsignacion(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<AsignacionResponse>($"api/Produccion/asignaciones/{id}");
        }

        public async Task<IEnumerable<ListaProduccionResponse>> ObtenerListaDiaria(Guid? idTrabajador = null)
        {
            var url = idTrabajador.HasValue
                ? $"api/Produccion/lista-diaria?Id_Trabajador={idTrabajador.Value}"
                : "api/Produccion/lista-diaria";

            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.NoContent) return [];
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ListaProduccionResponse>>() ?? [];
        }

        public async Task<IEnumerable<ProductoAsignadoResponse>> ObtenerProductosAsignados(Guid idTrabajador)
        {
            var response = await _httpClient.GetAsync($"api/Produccion/trabajadores/{idTrabajador}/productos");
            if (response.StatusCode == HttpStatusCode.NoContent) return [];
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ProductoAsignadoResponse>>() ?? [];
        }

        public async Task<(bool ok, string? error)> AgregarAsignacion(AsignacionRequest asignacion)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Produccion/asignaciones", asignacion);
            if (response.IsSuccessStatusCode) return (true, null);
            return (false, await response.Content.ReadAsStringAsync());
        }

        public async Task<(bool ok, string? error)> EditarAsignacion(Guid id, AsignacionRequest asignacion)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Produccion/asignaciones/{id}", asignacion);
            if (response.IsSuccessStatusCode) return (true, null);
            return (false, await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> EliminarAsignacion(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Produccion/asignaciones/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActivarAsignacion(Guid id)
        {
            var response = await _httpClient.PatchAsync($"api/Produccion/asignaciones/{id}/activar", null);
            return response.IsSuccessStatusCode;
        }
    }
}
