using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Web.Services;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccesoController : Controller
    {
        private readonly RegistroAccesoService _registroAccesoService;
        private readonly UsuarioService _usuarioService;

        public AccesoController(RegistroAccesoService registroAccesoService, UsuarioService usuarioService)
        {
            _registroAccesoService = registroAccesoService;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Historial(DateOnly? fechaInicio, DateOnly? fechaFin, string? nombreUsuario)
        {
            var (items, error) = await _registroAccesoService.Obtener(fechaInicio, fechaFin, nombreUsuario);
            var usuarios = await _usuarioService.Obtener();

            ViewBag.FechaInicio = fechaInicio;
            ViewBag.FechaFin = fechaFin;
            ViewBag.NombreUsuario = nombreUsuario;
            ViewBag.ErrorApi = error;
            ViewBag.Usuarios = usuarios
                .Select(u => u.Nombre_Usuario)
                .Distinct()
                .OrderBy(n => n)
                .ToList();

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(Guid id)
        {
            var registro = await _registroAccesoService.Obtener(id);
            if (registro == null) return NotFound();
            return View(registro);
        }

        [HttpGet]
        public async Task<IActionResult> ExportarCsv(DateOnly? fechaInicio, DateOnly? fechaFin, string? nombreUsuario)
        {
            var (items, error) = await _registroAccesoService.Obtener(fechaInicio, fechaFin, nombreUsuario);

            if (error != null)
                return BadRequest(error);

            var sb = new StringBuilder();
            sb.AppendLine("Usuario,Fecha Login,Fecha Logout,Estado");
            foreach (var r in items)
            {
                sb.AppendLine($"{r.Nombre_Usuario},{r.Fecha_Login:yyyy-MM-dd HH:mm:ss},{(r.Fecha_Logout.HasValue ? r.Fecha_Logout.Value.ToString("yyyy-MM-dd HH:mm:ss") : "")},{r.Estado}");
            }

            var bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(sb.ToString())).ToArray();
            return File(bytes, "text/csv", $"registro_accesos_{DateTime.Now:yyyyMMdd_HHmm}.csv");
        }
    }
}
