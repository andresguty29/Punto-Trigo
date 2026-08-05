using System.Net;
using System.Net.Http.Json;
using static Abstracciones.Modelos.Asistencia.Asistencia;

namespace Web.Services
{
    public class AsistenciaService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<(bool ok, string? error)> Registrar(AsistenciaRequest asistencia)
        {
            var respuesta = await _httpClient.PostAsJsonAsync("api/Asistencia", asistencia);
            if (respuesta.IsSuccessStatusCode) return (true, null);

            try
            {
                var body = await respuesta.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                return (false, body?.GetValueOrDefault("mensaje"));
            }
            catch { return (false, "No se pudo registrar la asistencia."); }
        }

        public async Task<IEnumerable<AsistenciaResponse>> Obtener(Guid idTrabajador)
        {
            var respuesta = await _httpClient.GetAsync($"api/Asistencia?idTrabajador={idTrabajador}");
            if (respuesta.StatusCode == HttpStatusCode.NoContent) return [];
            respuesta.EnsureSuccessStatusCode();
            return await respuesta.Content.ReadFromJsonAsync<IEnumerable<AsistenciaResponse>>() ?? [];
        }

        public async Task<ResumenAsistenciaResponse?> ObtenerResumen(Guid idTrabajador, DateOnly fechaInicio, DateOnly fechaFin)
        {
            var respuesta = await _httpClient.GetAsync($"api/Asistencia/resumen?idTrabajador={idTrabajador}&fechaInicio={fechaInicio:yyyy-MM-dd}&fechaFin={fechaFin:yyyy-MM-dd}");
            if (!respuesta.IsSuccessStatusCode) return null;
            return await respuesta.Content.ReadFromJsonAsync<ResumenAsistenciaResponse>();
        }
    }
}
