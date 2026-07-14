using System.Net;
using System.Net.Http.Json;
using static Abstracciones.Modelos.Tiquete.Tiquete;

namespace Web.Services
{
    public class TiqueteService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<IEnumerable<TiqueteResponse>> Obtener()
        {
            var response = await _httpClient.GetAsync("api/Tiquete");

            if (response.StatusCode == HttpStatusCode.NoContent)
                return [];

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<TiqueteResponse>>() ?? [];
        }

        public async Task<TiqueteResponse?> Obtener(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<TiqueteResponse>($"api/Tiquete/{id}");
        }

        public async Task<IEnumerable<DetalleTiqueteResponse>> ObtenerDetalle(Guid idTiquete)
        {
            var response = await _httpClient.GetAsync($"api/Tiquete/{idTiquete}/detalle");

            if (response.StatusCode == HttpStatusCode.NoContent)
                return [];

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<DetalleTiqueteResponse>>() ?? [];
        }

        public async Task<(bool ok, TiqueteResponse? tiquete, string? error)> Agregar(TiqueteRequest tiquete, Guid? idTrabajador)
        {
            var url = "api/Tiquete" + (idTrabajador.HasValue ? $"?idTrabajador={idTrabajador}" : "");
            var respuesta = await _httpClient.PostAsJsonAsync(url, tiquete);

            if (!respuesta.IsSuccessStatusCode)
                return (false, null, await LeerMensajeError(respuesta));

            var location = respuesta.Headers.Location;
            if (location == null) return (true, null, null);

            var idCreado = Guid.Parse(location.Segments.Last());
            var creado = await Obtener(idCreado);
            return (true, creado, null);
        }

        public async Task<(bool ok, string? error)> ReintentarEnvio(Guid id)
        {
            var respuesta = await _httpClient.PatchAsync($"api/Tiquete/{id}/reintentar", null);
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
