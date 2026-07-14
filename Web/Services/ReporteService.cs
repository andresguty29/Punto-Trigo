using System.Net.Http.Json;
using static Abstracciones.Modelos.Reporte.Reporte;

namespace Web.Services
{
    public class ReporteService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<(IEnumerable<ReporteDiaResponse> items, string? error)> Obtener(DateOnly fechaInicio, DateOnly fechaFin)
        {
            var response = await _httpClient.GetAsync($"api/Reporte?fechaInicio={fechaInicio:yyyy-MM-dd}&fechaFin={fechaFin:yyyy-MM-dd}");

            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    var body = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                    return ([], body?.GetValueOrDefault("mensaje") ?? "No se pudo generar el reporte.");
                }
                catch
                {
                    return ([], "No se pudo generar el reporte.");
                }
            }

            var items = await response.Content.ReadFromJsonAsync<IEnumerable<ReporteDiaResponse>>() ?? [];
            return (items, null);
        }
    }
}
