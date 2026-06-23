using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Services;
using static Abstracciones.Modelos.Trabajador.Trabajador;

namespace Web.Controllers
{
    public class TrabajadorController : Controller
    {
        private readonly TrabajadorService _trabajadorService;
        private readonly PuestoService _puestoService;

        public TrabajadorController(TrabajadorService trabajadorService, PuestoService puestoService)
        {
            _trabajadorService = trabajadorService;
            _puestoService = puestoService;
        }

        public async Task<IActionResult> Index()
        {
            var trabajadores = await _trabajadorService.Obtener();
            return View(trabajadores);
        }

        [HttpGet]
        public async Task<IActionResult> Crear(bool modal = false)
        {
            await CargarPuestos();
            ViewBag.Modal = modal;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(TrabajadorRequest trabajador, bool modal = false)
        {
            var (ok, error) = await _trabajadorService.Agregar(trabajador);

            if (!ok)
            {
                await CargarPuestos();
                ViewBag.Modal = modal;
                ViewBag.ErrorApi = error;
                return View(trabajador);
            }

            if (modal)
            {
                return Content(@"
            <script>
                window.parent.postMessage('crud-success', '*');
            </script>", "text/html");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid id, bool modal = false)
        {
            var trabajador = await _trabajadorService.Obtener(id);

            if (trabajador == null)
                return NotFound();

            var modelo = new TrabajadorRequest
            {
                Id_Trabajador = trabajador.Id_Trabajador,
                Cedula = trabajador.Cedula,
                Nombre_Completo = trabajador.Nombre_Completo,
                Id_Puesto = trabajador.Id_Puesto
            };

            await CargarPuestos();
            ViewBag.Modal = modal;
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, TrabajadorRequest trabajador, bool modal = false)
        {
            var (ok, error) = await _trabajadorService.Editar(id, trabajador);

            if (!ok)
            {
                await CargarPuestos();
                ViewBag.Modal = modal;
                ViewBag.ErrorApi = error;
                return View(trabajador);
            }

            if (modal)
            {
                return Content(@"
            <script>
                window.parent.postMessage('crud-success', '*');
            </script>", "text/html");
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Eliminar(Guid id)
        {
            await _trabajadorService.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Activar(Guid id)
        {
            await _trabajadorService.Activar(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task CargarPuestos()
        {
            var puestos = await _puestoService.Obtener();

            ViewBag.Puestos = puestos
                .Where(p => p.Id_Estado == 1)
                .Select(p => new SelectListItem
                {
                    Value = p.Id_Puesto.ToString(),
                    Text = p.Nombre_Puesto
                }).ToList();
        }
    }
}
