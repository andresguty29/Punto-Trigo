using Microsoft.AspNetCore.Mvc;
using Web.Services;
using static Abstracciones.Modelos.Puesto.Puesto;

namespace Web.Controllers
{
    public class PuestoController : Controller
    {
        private readonly PuestoService _puestoService;

        public PuestoController(PuestoService puestoService)
        {
            _puestoService = puestoService;
        }

        public async Task<IActionResult> Index()
        {
            var puestos = await _puestoService.Obtener();
            return View(puestos);
        }

        [HttpGet]
        public IActionResult Crear(bool modal = false)
        {
            ViewBag.Modal = modal;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(PuestoRequest puesto, bool modal = false)
        {
            var resultado = await _puestoService.Agregar(puesto);

            if (!resultado)
            {
                ViewBag.Modal = modal;
                return View(puesto);
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
            var puesto = await _puestoService.Obtener(id);

            if (puesto == null)
                return NotFound();

            var modelo = new PuestoRequest
            {
                Id_Puesto = puesto.Id_Puesto,
                Nombre_Puesto = puesto.Nombre_Puesto
            };

            ViewBag.Modal = modal;
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, PuestoRequest puesto, bool modal = false)
        {
            var resultado = await _puestoService.Editar(id, puesto);

            if (!resultado)
            {
                ViewBag.Modal = modal;
                return View(puesto);
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
            await _puestoService.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Activar(Guid id)
        {
            await _puestoService.Activar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
