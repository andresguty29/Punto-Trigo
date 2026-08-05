using System.Net.Http.Json;
using System.Security.Claims;
using static Abstracciones.Modelos.Bitacora.Bitacora;

namespace Web.Services
{
    public class BitacoraService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task Registrar(ClaimsPrincipal usuario, string accion, string? detalle = null)
        {
            var idClaim = usuario.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(idClaim, out var idUsuario);

            var nombreUsuario = usuario.Identity?.Name;

            try
            {
                await _httpClient.PostAsJsonAsync("api/Bitacora", new RegistrarBitacoraRequest
                {
                    Id_Usuario = idUsuario == Guid.Empty ? null : idUsuario,
                    Nombre_Usuario = string.IsNullOrWhiteSpace(nombreUsuario) ? "(desconocido)" : nombreUsuario,
                    Accion = accion,
                    Detalle = detalle
                });
            }
            catch { /* no debe interrumpir la operacion principal si falla el registro */ }
        }
    }
}
