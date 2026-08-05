using System.Net;
using System.Net.Http.Json;
using static Abstracciones.Modelos.Perdida.Perdida;

namespace Web.Services
{
    public class PerdidaService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<IEnumerable<VencimientoPendienteResponse>> ObtenerPendientes()
        {
            var respuesta = await _httpClient.GetAsync("api/Perdida/pendientes");
            if (respuesta.StatusCode == HttpStatusCode.NoContent) return [];
            respuesta.EnsureSuccessStatusCode();
            return await respuesta.Content.ReadFromJsonAsync<IEnumerable<VencimientoPendienteResponse>>() ?? [];
        }

        public async Task<(bool ok, string? error)> Procesar(Guid idMovimiento)
        {
            var respuesta = await _httpClient.PatchAsync($"api/Perdida/{idMovimiento}/procesar", null);
            if (respuesta.IsSuccessStatusCode) return (true, null);

            try
            {
                var body = await respuesta.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                return (false, body?.GetValueOrDefault("mensaje"));
            }
            catch { return (false, "No se pudo procesar el vencimiento."); }
        }
    }
}
