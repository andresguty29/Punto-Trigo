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

        public async Task<IActionResult> Crear()
        {
            await CargarPuestos();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(TrabajadorRequest trabajador)
        {
            var resultado = await _trabajadorService.Agregar(trabajador);

            if (!resultado)
            {
                await CargarPuestos();
                return View(trabajador);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(Guid id)
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

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, TrabajadorRequest trabajador)
        {
            var resultado = await _trabajadorService.Editar(id, trabajador);

            if (!resultado)
            {
                await CargarPuestos();
                return View(trabajador);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Eliminar(Guid id)
        {
            await _trabajadorService.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task CargarPuestos()
        {
            var puestos = await _puestoService.Obtener();

            ViewBag.Puestos = puestos.Select(p => new SelectListItem
            {
                Value = p.Id_Puesto.ToString(),
                Text = p.Nombre_Puesto
            }).ToList();
        }
    }
}