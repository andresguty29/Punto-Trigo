using System.Net;
using System.Net.Http.Json;
using static Abstracciones.Modelos.Vacacion.Vacacion;

namespace Web.Services
{
    public class VacacionService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<(bool ok, IEnumerable<VacacionAsignadaResponse> resultado, string? error)> Asignar(Guid idTrabajador)
        {
            var respuesta = await _httpClient.PostAsync($"api/Vacacion?idTrabajador={idTrabajador}", null);

            if (respuesta.IsSuccessStatusCode)
                return (true, await respuesta.Content.ReadFromJsonAsync<IEnumerable<VacacionAsignadaResponse>>() ?? [], null);

            try
            {
                var body = await respuesta.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                return (false, [], body?.GetValueOrDefault("mensaje"));
            }
            catch { return (false, [], "No se pudo asignar vacaciones."); }
        }

        public async Task<IEnumerable<VacacionAsignadaResponse>> Obtener(Guid idTrabajador)
        {
            var respuesta = await _httpClient.GetAsync($"api/Vacacion?idTrabajador={idTrabajador}");
            if (respuesta.StatusCode == HttpStatusCode.NoContent) return [];
            respuesta.EnsureSuccessStatusCode();
            return await respuesta.Content.ReadFromJsonAsync<IEnumerable<VacacionAsignadaResponse>>() ?? [];
        }
    }
}
