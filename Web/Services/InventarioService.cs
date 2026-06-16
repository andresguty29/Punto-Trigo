using System.Net;
using System.Net.Http.Json;
using static Abstracciones.Modelos.Inventario.Inventario;
using static Abstracciones.Modelos.Inventario.Movimiento;

namespace Web.Services
{
    public class InventarioService(HttpClient httpClient)
        : CrudService<InventarioResponse, InventarioRequest>(httpClient)
    {
        protected override string Ruta => "Inventario";

        public async Task<Guid> RegistrarMovimiento(MovimientoRequest movimiento)
        {
            var respuesta = await _httpClient.PostAsJsonAsync("api/Inventario/movimiento", movimiento);
            respuesta.EnsureSuccessStatusCode();
            return await respuesta.Content.ReadFromJsonAsync<Guid>();
        }

        public async Task<IEnumerable<MovimientoResponse>> ObtenerMovimientos(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Inventario/{id}/movimientos");
            if (response.StatusCode == HttpStatusCode.NoContent) return [];
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<MovimientoResponse>>() ?? [];
        }
    }
}
