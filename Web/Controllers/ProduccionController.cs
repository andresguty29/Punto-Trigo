using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Services;
using static Abstracciones.Modelos.Produccion.Produccion;

namespace Web.Controllers
{
    public class ProduccionController : Controller
    {
        private readonly ProduccionService _produccionService;
        private readonly TrabajadorService _trabajadorService;
        private readonly ProductoService _productoService;

        public ProduccionController(
            ProduccionService produccionService,
            TrabajadorService trabajadorService,
            ProductoService productoService)
        {
            _produccionService = produccionService;
            _trabajadorService = trabajadorService;
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> Crear(bool modal = false)
        {
            await CargarCombos();
            ViewBag.Modal = modal;
            return View(new AsignacionRequest());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(AsignacionRequest asignacion, bool modal = false)
        {
            var (ok, error) = await _produccionService.AgregarAsignacion(asignacion);

            if (!ok)
            {
                await CargarCombos();
                ViewBag.Modal = modal;
                ViewBag.ErrorApi = string.IsNullOrWhiteSpace(error)
                    ? "No se pudo guardar la asignacion. Verifica que no exista duplicada."
                    : error;
                return View(asignacion);
            }

            if (modal)
            {
                return Content(@"
                <script>
                    window.parent.postMessage('crud-success', '*');
                </script>", "text/html");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid id, bool modal = false)
        {
            var asignacion = await _produccionService.ObtenerAsignacion(id);
            if (asignacion == null) return NotFound();

            await CargarCombos();
            ViewBag.Modal = modal;

            return View(new AsignacionRequest
            {
                Id_Asignacion = asignacion.Id_Asignacion,
                Id_Trabajador = asignacion.Id_Trabajador,
                Id_Producto = asignacion.Id_Producto,
                Cantidad_Diaria = asignacion.Cantidad_Diaria
            });
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, AsignacionRequest asignacion, bool modal = false)
        {
            var (ok, error) = await _produccionService.EditarAsignacion(id, asignacion);

            if (!ok)
            {
                await CargarCombos();
                ViewBag.Modal = modal;
                ViewBag.ErrorApi = string.IsNullOrWhiteSpace(error)
                    ? "No se pudo actualizar la asignacion. Revisa que no exista otra igual."
                    : error;
                return View(asignacion);
            }

            if (modal)
            {
                return Content(@"
                <script>
                    window.parent.postMessage('crud-success', '*');
                </script>", "text/html");
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Eliminar(Guid id)
        {
            await _produccionService.EliminarAsignacion(id);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Activar(Guid id)
        {
            await _produccionService.ActivarAsignacion(id);
            return RedirectToAction("Index", "Home");
        }

        private async Task CargarCombos()
        {
            var trabajadores = await _trabajadorService.Obtener();
            var productos = await _productoService.Obtener();

            ViewBag.Trabajadores = trabajadores
                .Where(t => t.Id_Estado == 1)
                .OrderBy(t => t.Nombre_Completo)
                .Select(t => new SelectListItem
                {
                    Value = t.Id_Trabajador.ToString(),
                    Text = t.Nombre_Completo
                }).ToList();

            ViewBag.Productos = productos
                .Where(p => p.Id_Estado == 1)
                .OrderBy(p => p.Nombre_Producto)
                .Select(p => new SelectListItem
                {
                    Value = p.Id_Producto.ToString(),
                    Text = p.Nombre_Producto
                }).ToList();
        }
    }
}
