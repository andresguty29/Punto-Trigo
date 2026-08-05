using System.Net;
using System.Net.Http.Json;
using static Abstracciones.Modelos.HorasExtra.HorasExtra;

namespace Web.Services
{
    public class HorasExtraService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<(bool ok, string? error)> Registrar(HorasExtraRequest horasExtra)
        {
            var respuesta = await _httpClient.PostAsJsonAsync("api/HorasExtra", horasExtra);
            if (respuesta.IsSuccessStatusCode) return (true, null);

            try
            {
                var body = await respuesta.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                return (false, body?.GetValueOrDefault("mensaje"));
            }
            catch { return (false, "No se pudo registrar las horas adicionales."); }
        }

        public async Task<IEnumerable<HorasExtraResponse>> Obtener(Guid idTrabajador)
        {
            var respuesta = await _httpClient.GetAsync($"api/HorasExtra?idTrabajador={idTrabajador}");
            if (respuesta.StatusCode == HttpStatusCode.NoContent) return [];
            respuesta.EnsureSuccessStatusCode();
            return await respuesta.Content.ReadFromJsonAsync<IEnumerable<HorasExtraResponse>>() ?? [];
        }
    }
}
