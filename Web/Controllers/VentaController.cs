using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Services;
using static Abstracciones.Modelos.Tiquete.Tiquete;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin,Cajas")]
    public class VentaController : Controller
    {
        private readonly TiqueteService _tiqueteService;
        private readonly ClienteService _clienteService;

        public VentaController(TiqueteService tiqueteService, ClienteService clienteService)
        {
            _tiqueteService = tiqueteService;
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarCliente(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
                return Json(new { encontrado = false });

            var clientes = await _clienteService.Obtener();
            var cliente = clientes.FirstOrDefault(c => c.Cedula == cedula && c.Id_Estado == 1);

            if (cliente == null)
                return Json(new { encontrado = false });

            return Json(new { encontrado = true, id = cliente.Id_Cliente, nombre = cliente.Nombre_Completo });
        }

        [HttpPost]
        public async Task<IActionResult> Cobrar([FromBody] TiqueteRequest venta)
        {
            var idClaim = User.FindFirst("Id_Trabajador")?.Value;
            Guid? idTrabajador = Guid.TryParse(idClaim, out var id) ? id : null;

            var (ok, tiquete, error) = await _tiqueteService.Agregar(venta, idTrabajador);

            if (!ok)
            {
                return Json(new { ok = false, mensaje = string.IsNullOrWhiteSpace(error) ? "No se pudo registrar la venta." : error });
            }

            return Json(new
            {
                ok = true,
                idTiquete = tiquete?.Id_Tiquete,
                consecutivo = tiquete?.Consecutivo,
                clave = tiquete?.Clave,
                estado = tiquete?.Estado,
                montoTotal = tiquete?.Monto_Total,
                nombreCliente = tiquete?.Nombre_Cliente
            });
        }

        [HttpPost]
        public async Task<IActionResult> Reintentar(Guid idTiquete)
        {
            var (ok, error) = await _tiqueteService.ReintentarEnvio(idTiquete);
            return Json(new { ok, mensaje = error });
        }
    }
}
