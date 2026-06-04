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

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(PuestoRequest puesto)
        {
            var resultado = await _puestoService.Agregar(puesto);

            if (!resultado)
                return View(puesto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(Guid id)
        {
            var puesto = await _puestoService.Obtener(id);

            if (puesto == null)
                return NotFound();

            var modelo = new PuestoRequest
            {
                Id_Puesto = puesto.Id_Puesto,
                Nombre_Puesto = puesto.Nombre_Puesto
            };

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, PuestoRequest puesto)
        {
            var resultado = await _puestoService.Editar(id, puesto);

            if (!resultado)
                return View(puesto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Eliminar(Guid id)
        {
            await _puestoService.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}