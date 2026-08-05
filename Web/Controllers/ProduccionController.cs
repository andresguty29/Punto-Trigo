using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Web.Services;
using static Abstracciones.Modelos.Produccion.Produccion;
using static Abstracciones.Modelos.Inventario.Movimiento;

namespace Web.Controllers
{
    [Authorize]
    public class ProduccionController : Controller
    {
        private readonly ProduccionService _produccionService;
        private readonly TrabajadorService _trabajadorService;
        private readonly ProductoService _productoService;
        private readonly InventarioService _inventarioService;

        public ProduccionController(
            ProduccionService produccionService,
            TrabajadorService trabajadorService,
            ProductoService productoService,
            InventarioService inventarioService)
        {
            _produccionService = produccionService;
            _trabajadorService = trabajadorService;
            _productoService = productoService;
            _inventarioService = inventarioService;
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

            var materiales = await _produccionService.ObtenerMateriales(id);

            await CargarCombos();
            ViewBag.Modal = modal;

            return View(new AsignacionRequest
            {
                Id_Asignacion = asignacion.Id_Asignacion,
                Id_Trabajador = asignacion.Id_Trabajador,
                Id_Producto = asignacion.Id_Producto,
                Cantidad_Diaria = asignacion.Cantidad_Diaria,
                Materiales = materiales.Select(m => new MaterialAsignacionRequest
                {
                    Id_Inventario = m.Id_Inventario,
                    Cantidad = m.Cantidad
                }).ToList()
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

        [HttpGet]
        public async Task<IActionResult> MiLista()
        {
            var idClaim = User.FindFirst("Id_Trabajador")?.Value;
            if (!Guid.TryParse(idClaim, out Guid idTrabajador))
                return Unauthorized();

            var lista = await _produccionService.ObtenerListaDiaria(idTrabajador);

            var listaConMateriales = new List<(ListaProduccionResponse Asignacion, IEnumerable<MaterialAsignacionResponse> Materiales)>();
            foreach (var item in lista)
            {
                var materiales = await _produccionService.ObtenerMateriales(item.Id_Asignacion);
                listaConMateriales.Add((item, materiales));
            }

            return View(listaConMateriales);
        }

        [HttpPost]
        public async Task<IActionResult> Realizar(Guid id)
        {
            var asignacion = await _produccionService.ObtenerAsignacion(id);
            if (asignacion == null) return NotFound();

            var materiales = await _produccionService.ObtenerMateriales(id);

            try
            {
                foreach (var material in materiales)
                {
                    await _inventarioService.RegistrarMovimiento(new MovimientoRequest
                    {
                        Id_Inventario = material.Id_Inventario,
                        Tipo = "Salida",
                        Cantidad = material.Cantidad,
                        Motivo = $"Produccion: {asignacion.Nombre_Producto}"
                    });
                }

                await _produccionService.MarcarRealizada(id);
                TempData["ProduccionOk"] = "Produccion marcada como realizada. Se descontaron los materiales del inventario.";
            }
            catch (Exception ex)
            {
                TempData["ProduccionError"] = ex.Message.Contains("Stock insuficiente")
                    ? "No se pudo completar: no hay suficiente stock de uno o mas materiales."
                    : "No se pudo marcar la produccion como realizada. Intenta de nuevo.";
            }

            return RedirectToAction(nameof(MiLista));
        }

        [HttpGet]
        public async Task<IActionResult> Imprimir(Guid id)
        {
            var asignacion = await _produccionService.ObtenerAsignacion(id);
            if (asignacion == null) return NotFound();

            var materiales = await _produccionService.ObtenerMateriales(id);

            ViewBag.Materiales = materiales;
            return View(asignacion);
        }

        public static readonly string[] TurnosValidos = ["Mañana", "Tarde", "Noche"];

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SeleccionarListaEmpleado(bool modal = true)
        {
            var panaderos = await _trabajadorService.ObtenerPanaderos();

            ViewBag.Trabajadores = panaderos
                .OrderBy(t => t.Nombre_Completo)
                .Select(t => new SelectListItem
                {
                    Value = t.Id_Trabajador.ToString(),
                    Text = t.Nombre_Completo
                }).ToList();

            ViewBag.Turnos = TurnosValidos;
            ViewBag.Modal = modal;
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ImprimirListaEmpleado(Guid? idTrabajador, string? turno)
        {
            if (idTrabajador == null || string.IsNullOrWhiteSpace(turno))
            {
                ViewBag.MensajeEstado = "Debes seleccionar un empleado y un turno para generar la lista.";
                ViewBag.EsAdvertencia = true;
                return View("ListaEmpleadoMensaje");
            }

            if (!TurnosValidos.Contains(turno))
            {
                ViewBag.MensajeEstado = $"El turno \"{turno}\" no es válido. Los turnos permitidos son: {string.Join(", ", TurnosValidos)}.";
                ViewBag.EsAdvertencia = false;
                return View("ListaEmpleadoMensaje");
            }

            try
            {
                var panaderos = await _trabajadorService.ObtenerPanaderos();
                var empleado = panaderos.FirstOrDefault(p => p.Id_Trabajador == idTrabajador);

                if (empleado == null)
                {
                    ViewBag.MensajeEstado = "El empleado seleccionado no existe o no está activo.";
                    ViewBag.EsAdvertencia = false;
                    return View("ListaEmpleadoMensaje");
                }

                var lista = await _produccionService.ObtenerListaDiaria(idTrabajador.Value);

                if (!lista.Any())
                {
                    ViewBag.MensajeEstado = $"{empleado.Nombre_Completo} no tiene productos de producción asignados para hoy.";
                    ViewBag.EsAdvertencia = true;
                    return View("ListaEmpleadoMensaje");
                }

                ViewBag.NombreEmpleado = empleado.Nombre_Completo;
                ViewBag.Turno = turno;
                ViewBag.Fecha = DateTime.Now;
                return View(lista);
            }
            catch
            {
                ViewBag.MensajeEstado = "No se pudo generar el documento. Intenta de nuevo.";
                ViewBag.EsAdvertencia = false;
                return View("ListaEmpleadoMensaje");
            }
        }

        private async Task CargarCombos()
        {
            var panaderos = await _trabajadorService.ObtenerPanaderos();
            var productos = await _productoService.Obtener();
            var inventario = await _inventarioService.Obtener();

            ViewBag.Trabajadores = panaderos
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

            ViewBag.Inventario = inventario
                .Where(i => i.Id_Estado == 1 && i.Stock_Actual > 0)
                .OrderBy(i => i.Nombre)
                .Select(i => new SelectListItem
                {
                    Value = i.Id_Inventario.ToString(),
                    Text = $"{i.Nombre} ({i.Unidad})"
                }).ToList();
        }
    }
}
