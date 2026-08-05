using System.Net;
using System.Net.Http.Json;
using static Abstracciones.Modelos.Trabajador.Trabajador;

namespace Web.Services
{
    public class TrabajadorService(HttpClient httpClient)
        : CrudService<TrabajadorResponse, TrabajadorRequest>(httpClient)
    {
        protected override string Ruta => "Trabajador";

        public async Task<IEnumerable<TrabajadorResponse>> ObtenerPanaderos()
        {
            var response = await _httpClient.GetAsync("api/Trabajador/panaderos");
            if (response.StatusCode == HttpStatusCode.NoContent) return [];
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<TrabajadorResponse>>() ?? [];
        }

        public async Task<(bool ok, string? error)> ConfigurarPago(Guid id, ConfigurarPagoRequest configuracion)
        {
            var respuesta = await _httpClient.PatchAsJsonAsync($"api/Trabajador/{id}/pago", configuracion);
            if (respuesta.IsSuccessStatusCode) return (true, null);

            try
            {
                var body = await respuesta.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                return (false, body?.GetValueOrDefault("mensaje"));
            }
            catch { return (false, null); }
        }

        public async Task<(CalculoPagoResponse? resultado, string? error)> CalcularPago(Guid id, decimal? horasTrabajadas)
        {
            var url = $"api/Trabajador/{id}/pago/calcular" + (horasTrabajadas.HasValue ? $"?horasTrabajadas={horasTrabajadas}" : "");
            var respuesta = await _httpClient.GetAsync(url);

            if (respuesta.IsSuccessStatusCode)
                return (await respuesta.Content.ReadFromJsonAsync<CalculoPagoResponse>(), null);

            try
            {
                var body = await respuesta.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                return (null, body?.GetValueOrDefault("mensaje"));
            }
            catch { return (null, "No se pudo calcular el pago."); }
        }
    }
}
