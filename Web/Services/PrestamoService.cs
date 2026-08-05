using System.Net;
using System.Net.Http.Json;
using static Abstracciones.Modelos.Prestamo.Prestamo;

namespace Web.Services
{
    public class PrestamoService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<(bool ok, string? error)> Registrar(PrestamoRequest prestamo)
        {
            var respuesta = await _httpClient.PostAsJsonAsync("api/Prestamo", prestamo);
            if (respuesta.IsSuccessStatusCode) return (true, null);

            try
            {
                var body = await respuesta.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                return (false, body?.GetValueOrDefault("mensaje"));
            }
            catch { return (false, "No se pudo registrar el préstamo."); }
        }

        public async Task<IEnumerable<PrestamoResponse>> Obtener(Guid idTrabajador)
        {
            var respuesta = await _httpClient.GetAsync($"api/Prestamo?idTrabajador={idTrabajador}");
            if (respuesta.StatusCode == HttpStatusCode.NoContent) return [];
            respuesta.EnsureSuccessStatusCode();
            return await respuesta.Content.ReadFromJsonAsync<IEnumerable<PrestamoResponse>>() ?? [];
        }
    }
}
