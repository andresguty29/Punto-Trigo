using System.Net;
using System.Net.Http.Json;
using static Abstracciones.Modelos.RegistroAcceso.RegistroAcceso;

namespace Web.Services
{
    public class RegistroAccesoService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<Guid?> Registrar(RegistrarAccesoRequest registro)
        {
            try
            {
                var respuesta = await _httpClient.PostAsJsonAsync("api/RegistroAcceso", registro);
                if (!respuesta.IsSuccessStatusCode) return null;
                return await respuesta.Content.ReadFromJsonAsync<Guid>();
            }
            catch
            {
                // No debe impedir el login/logout si falla el registro de auditoria
                return null;
            }
        }

        public async Task CerrarSesion(Guid idRegistro)
        {
            try
            {
                await _httpClient.PatchAsync($"api/RegistroAcceso/{idRegistro}/cerrar", null);
            }
            catch { /* best effort */ }
        }

        public async Task<(IEnumerable<RegistroAccesoResponse> items, string? error)> Obtener(DateOnly? fechaInicio = null, DateOnly? fechaFin = null, string? nombreUsuario = null)
        {
            var query = new List<string>();
            if (fechaInicio.HasValue) query.Add($"fechaInicio={fechaInicio:yyyy-MM-dd}");
            if (fechaFin.HasValue) query.Add($"fechaFin={fechaFin:yyyy-MM-dd}");
            if (!string.IsNullOrWhiteSpace(nombreUsuario)) query.Add($"nombreUsuario={Uri.EscapeDataString(nombreUsuario)}");

            var url = "api/RegistroAcceso" + (query.Count > 0 ? "?" + string.Join("&", query) : "");

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.NoContent)
                    return ([], null);

                if (!response.IsSuccessStatusCode)
                    return ([], "No se pudo consultar el registro de accesos.");

                var items = await response.Content.ReadFromJsonAsync<IEnumerable<RegistroAccesoResponse>>() ?? [];
                return (items, null);
            }
            catch
            {
                return ([], "No se pudo comunicar con el servidor. Intenta de nuevo.");
            }
        }

        public async Task<RegistroAccesoResponse?> Obtener(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<RegistroAccesoResponse>($"api/RegistroAcceso/{id}");
        }
    }
}
