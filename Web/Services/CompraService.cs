using System.Net;
using System.Net.Http.Json;
using static Abstracciones.Modelos.Compra.Compra;

namespace Web.Services
{
    public class CompraService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<(IEnumerable<CompraResponse> items, string? error)> Obtener(DateOnly? fechaInicio = null, DateOnly? fechaFin = null, decimal? montoMinimo = null)
        {
            var query = new List<string>();
            if (fechaInicio.HasValue) query.Add($"fechaInicio={fechaInicio:yyyy-MM-dd}");
            if (fechaFin.HasValue) query.Add($"fechaFin={fechaFin:yyyy-MM-dd}");
            if (montoMinimo.HasValue) query.Add($"montoMinimo={montoMinimo}");

            var url = "api/Compra" + (query.Count > 0 ? "?" + string.Join("&", query) : "");
            var response = await _httpClient.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.NoContent)
                return ([], null);

            if (!response.IsSuccessStatusCode)
                return ([], await LeerMensajeError(response));

            var items = await response.Content.ReadFromJsonAsync<IEnumerable<CompraResponse>>() ?? [];
            return (items, null);
        }

        public async Task<CompraResponse?> Obtener(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<CompraResponse>($"api/Compra/{id}");
        }

        public async Task<IEnumerable<DetalleCompraResponse>> ObtenerDetalle(Guid idCompra)
        {
            var response = await _httpClient.GetAsync($"api/Compra/{idCompra}/detalle");

            if (response.StatusCode == HttpStatusCode.NoContent)
                return [];

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<DetalleCompraResponse>>() ?? [];
        }

        public async Task<(bool ok, string? error)> Agregar(CompraRequest compra)
        {
            var respuesta = await _httpClient.PostAsJsonAsync("api/Compra", compra);
            if (respuesta.IsSuccessStatusCode) return (true, null);
            return (false, await LeerMensajeError(respuesta));
        }

        public async Task<bool> Anular(Guid id)
        {
            var respuesta = await _httpClient.PatchAsync($"api/Compra/{id}/anular", null);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<(bool ok, string? error)> Reclasificar(Guid id, ReclasificarCompraRequest reclasificacion)
        {
            var respuesta = await _httpClient.PatchAsJsonAsync($"api/Compra/{id}/reclasificar", reclasificacion);
            if (respuesta.IsSuccessStatusCode) return (true, null);
            return (false, await LeerMensajeError(respuesta));
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
    }
}
