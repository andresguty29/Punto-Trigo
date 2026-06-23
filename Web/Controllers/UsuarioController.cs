using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Services;
using static Abstracciones.Modelos.Usuario.Usuario;

namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly TrabajadorService _trabajadorService;

        public UsuarioController(UsuarioService usuarioService, TrabajadorService trabajadorService)
        {
            _usuarioService = usuarioService;
            _trabajadorService = trabajadorService;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioService.Obtener();
            return View(usuarios);
        }

        [HttpGet]
        public async Task<IActionResult> Crear(bool modal = false)
        {
            await CargarTrabajadores();
            ViewBag.Modal = modal;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(UsuarioRequest usuario, bool modal = false)
        {
            var (ok, error) = await _usuarioService.Agregar(usuario);

            if (!ok)
            {
                await CargarTrabajadores();
                ViewBag.Modal = modal;
                ViewBag.ErrorApi = error;
                return View(usuario);
            }

            if (modal)
            {
                return Content(@"
            <script>
                window.parent.postMessage('crud-success', '*');
            </script>",
                    "text/html");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid id, bool modal = false)
        {
            var usuario = await _usuarioService.Obtener(id);

            if (usuario == null)
                return NotFound();

            var modelo = new UsuarioRequest
            {
                Id_Usuario     = usuario.Id_Usuario,
                Nombre_Usuario = usuario.Nombre_Usuario,
                Contrasena     = null,
                Id_Trabajador  = usuario.Id_Trabajador,
                Rol            = usuario.Rol
            };

            await CargarTrabajadores();
            ViewBag.Modal = modal;

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, UsuarioRequest usuario, bool modal = false)
        {
            var (ok, error) = await _usuarioService.Editar(id, usuario);

            if (!ok)
            {
                await CargarTrabajadores();
                ViewBag.Modal = modal;
                ViewBag.ErrorApi = error;
                return View(usuario);
            }

            if (modal)
            {
                return Content(@"
            <script>
                window.parent.postMessage('crud-success', '*');
            </script>",
                    "text/html");
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Eliminar(Guid id)
        {
            await _usuarioService.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Activar(Guid id)
        {
            await _usuarioService.Activar(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task CargarTrabajadores()
        {
            var trabajadores = await _trabajadorService.Obtener();

            ViewBag.Trabajadores = trabajadores
                .Where(t => t.Id_Estado == 1)
                .Select(t => new SelectListItem
                {
                    Value = t.Id_Trabajador.ToString(),
                    Text = t.Nombre_Completo
                }).ToList();
        }
    }
}
