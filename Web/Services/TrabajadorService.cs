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
    }
}
