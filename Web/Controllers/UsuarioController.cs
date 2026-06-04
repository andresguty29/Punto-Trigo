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

        public async Task<IActionResult> Crear()
        {
            await CargarTrabajadores();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(UsuarioRequest usuario)
        {
            var resultado = await _usuarioService.Agregar(usuario);

            if (!resultado)
            {
                await CargarTrabajadores();
                return View(usuario);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(Guid id)
        {
            var usuario = await _usuarioService.Obtener(id);

            if (usuario == null)
                return NotFound();

            var modelo = new UsuarioRequest
            {
                Id_Usuario = usuario.Id_Usuario,
                Nombre_Usuario = usuario.Nombre_Usuario,
                Contrasena = usuario.Contrasena,
                Id_Trabajador = usuario.Id_Trabajador
            };

            await CargarTrabajadores();

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, UsuarioRequest usuario)
        {
            var resultado = await _usuarioService.Editar(id, usuario);

            if (!resultado)
            {
                await CargarTrabajadores();
                return View(usuario);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Eliminar(Guid id)
        {
            await _usuarioService.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task CargarTrabajadores()
        {
            var trabajadores = await _trabajadorService.Obtener();

            ViewBag.Trabajadores = trabajadores.Select(t => new SelectListItem
            {
                Value = t.Id_Trabajador.ToString(),
                Text = t.Nombre_Completo
            }).ToList();
        }
    }
}