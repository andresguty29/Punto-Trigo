using System.Net;
using System.Net.Http.Json;
using static Abstracciones.Modelos.Planilla.Planilla;

namespace Web.Services
{
    public class PlanillaService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<(bool ok, Guid? idPlanilla, string? error)> GenerarDetallePago(GenerarDetallePagoRequest request)
        {
            var respuesta = await _httpClient.PostAsJsonAsync("api/Planilla", request);

            if (respuesta.IsSuccessStatusCode)
            {
                var idPlanilla = await respuesta.Content.ReadFromJsonAsync<Guid>();
                return (true, idPlanilla, null);
            }

            try
            {
                var body = await respuesta.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                return (false, null, body?.GetValueOrDefault("mensaje"));
            }
            catch { return (false, null, "No se pudo generar el detalle de pago."); }
        }

        public async Task<DetallePagoResponse?> ObtenerDetalle(Guid idPlanilla)
        {
            return await _httpClient.GetFromJsonAsync<DetallePagoResponse>($"api/Planilla/{idPlanilla}");
        }

        public async Task<(IEnumerable<DetallePagoResponse> items, string? error)> ObtenerHistorial(DateOnly? fechaInicio, DateOnly? fechaFin)
        {
            var query = new List<string>();
            if (fechaInicio.HasValue) query.Add($"fechaInicio={fechaInicio:yyyy-MM-dd}");
            if (fechaFin.HasValue) query.Add($"fechaFin={fechaFin:yyyy-MM-dd}");

            var url = "api/Planilla" + (query.Count > 0 ? "?" + string.Join("&", query) : "");
            var respuesta = await _httpClient.GetAsync(url);

            if (respuesta.StatusCode == HttpStatusCode.NoContent)
                return ([], null);

            if (!respuesta.IsSuccessStatusCode)
                return ([], "No se pudo consultar el historial de salarios.");

            var items = await respuesta.Content.ReadFromJsonAsync<IEnumerable<DetallePagoResponse>>() ?? [];
            return (items, null);
        }
    }
}
