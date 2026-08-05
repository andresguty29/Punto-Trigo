using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Services;
using static Abstracciones.Modelos.Asistencia.Asistencia;
using static Abstracciones.Modelos.HorasExtra.HorasExtra;
using static Abstracciones.Modelos.Prestamo.Prestamo;
using static Abstracciones.Modelos.Trabajador.Trabajador;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TrabajadorController : Controller
    {
        private readonly TrabajadorService _trabajadorService;
        private readonly PuestoService _puestoService;
        private readonly VacacionService _vacacionService;
        private readonly BitacoraService _bitacoraService;
        private readonly AsistenciaService _asistenciaService;
        private readonly PrestamoService _prestamoService;
        private readonly HorasExtraService _horasExtraService;
        private readonly PlanillaService _planillaService;

        public TrabajadorController(
            TrabajadorService trabajadorService,
            PuestoService puestoService,
            VacacionService vacacionService,
            BitacoraService bitacoraService,
            AsistenciaService asistenciaService,
            PrestamoService prestamoService,
            HorasExtraService horasExtraService,
            PlanillaService planillaService)
        {
            _trabajadorService = trabajadorService;
            _puestoService = puestoService;
            _vacacionService = vacacionService;
            _bitacoraService = bitacoraService;
            _asistenciaService = asistenciaService;
            _prestamoService = prestamoService;
            _horasExtraService = horasExtraService;
            _planillaService = planillaService;
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
                Id_Puesto = trabajador.Id_Puesto,
                Fecha_Ingreso = trabajador.Fecha_Ingreso
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

        [HttpGet]
        public async Task<IActionResult> ConfigurarPago(Guid id, bool modal = false)
        {
            var trabajador = await _trabajadorService.Obtener(id);
            if (trabajador == null) return NotFound();

            ViewBag.NombreTrabajador = trabajador.Nombre_Completo;
            ViewBag.IdTrabajador = id;
            ViewBag.Modal = modal;
            ViewBag.TiposPago = TiposPagoValidos;

            return View(new ConfigurarPagoRequest
            {
                Tipo_Pago = trabajador.Tipo_Pago,
                Salario_Base = trabajador.Salario_Base,
                Tarifa_Hora = trabajador.Tarifa_Hora
            });
        }

        [HttpPost]
        public async Task<IActionResult> ConfigurarPago(Guid id, ConfigurarPagoRequest configuracion, bool modal = false)
        {
            var (ok, error) = await _trabajadorService.ConfigurarPago(id, configuracion);

            if (!ok)
            {
                var trabajador = await _trabajadorService.Obtener(id);
                ViewBag.NombreTrabajador = trabajador?.Nombre_Completo;
                ViewBag.IdTrabajador = id;
                ViewBag.Modal = modal;
                ViewBag.TiposPago = TiposPagoValidos;
                ViewBag.ErrorApi = string.IsNullOrWhiteSpace(error)
                    ? "No se pudo guardar la configuración de pago."
                    : error;
                return View(configuracion);
            }

            await _bitacoraService.Registrar(User, "ConfigurarTipoPago", $"Configuró el tipo de pago ({configuracion.Tipo_Pago}) para el trabajador {id}.");

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
        public async Task<IActionResult> Vacaciones(Guid id)
        {
            var trabajador = await _trabajadorService.Obtener(id);
            if (trabajador == null) return NotFound();

            var vacaciones = await _vacacionService.Obtener(id);

            ViewBag.NombreTrabajador = trabajador.Nombre_Completo;
            ViewBag.FechaIngreso = trabajador.Fecha_Ingreso;
            ViewBag.IdTrabajador = id;

            return View(vacaciones);
        }

        [HttpPost]
        public async Task<IActionResult> AsignarVacaciones(Guid id)
        {
            var (ok, resultado, error) = await _vacacionService.Asignar(id);

            if (ok)
            {
                var lista = resultado.OrderBy(r => r.Anio_Antiguedad).ToList();
                var totalDias = lista.Sum(r => r.Dias_Asignados);
                var anios = string.Join(", ", lista.Select(r => r.Anio_Antiguedad));

                await _bitacoraService.Registrar(User, "AsignarVacaciones", $"Asignó {totalDias} días de vacaciones (año(s) {anios}) al trabajador {id}.");
                TempData["VacacionOk"] = lista.Count == 1
                    ? $"Se asignaron {totalDias} días de vacaciones correspondientes al año {anios} de antigüedad."
                    : $"Se asignaron {totalDias} días de vacaciones en total, correspondientes a los años {anios} de antigüedad pendientes.";
            }
            else
            {
                TempData["VacacionError"] = error ?? "No se pudo asignar vacaciones.";
            }

            return RedirectToAction(nameof(Vacaciones), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Asistencia(Guid id)
        {
            var trabajador = await _trabajadorService.Obtener(id);
            if (trabajador == null) return NotFound();

            var registros = await _asistenciaService.Obtener(id);

            var hoy = DateOnly.FromDateTime(DateTime.Today);
            var inicioMes = new DateOnly(hoy.Year, hoy.Month, 1);
            var resumen = await _asistenciaService.ObtenerResumen(id, inicioMes, hoy);

            ViewBag.NombreTrabajador = trabajador.Nombre_Completo;
            ViewBag.IdTrabajador = id;
            ViewBag.Resumen = resumen;
            ViewBag.Tipos = TiposEventoValidos;

            return View(registros);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarAsistencia(AsistenciaRequest asistencia)
        {
            var (ok, error) = await _asistenciaService.Registrar(asistencia);

            if (ok)
            {
                await _bitacoraService.Registrar(User, "RegistrarAsistencia", $"Registró {asistencia.Tipo_Evento} para el trabajador {asistencia.Id_Trabajador} el {asistencia.Fecha:dd/MM/yyyy}.");
                TempData["AsistenciaOk"] = "Registro de asistencia guardado correctamente.";
            }
            else
            {
                TempData["AsistenciaError"] = error ?? "No se pudo registrar la asistencia.";
            }

            return RedirectToAction(nameof(Asistencia), new { id = asistencia.Id_Trabajador });
        }

        [HttpGet]
        public async Task<IActionResult> Prestamos(Guid id)
        {
            var trabajador = await _trabajadorService.Obtener(id);
            if (trabajador == null) return NotFound();

            var registros = await _prestamoService.Obtener(id);

            ViewBag.NombreTrabajador = trabajador.Nombre_Completo;
            ViewBag.IdTrabajador = id;

            return View(registros);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarPrestamo(PrestamoRequest prestamo)
        {
            var (ok, error) = await _prestamoService.Registrar(prestamo);

            if (ok)
            {
                await _bitacoraService.Registrar(User, "RegistrarPrestamo", $"Registró un préstamo de ₡{prestamo.Monto:N2} para el trabajador {prestamo.Id_Trabajador}.");
                TempData["PrestamoOk"] = "Préstamo/adelanto registrado correctamente.";
            }
            else
            {
                TempData["PrestamoError"] = error ?? "No se pudo registrar el préstamo.";
            }

            return RedirectToAction(nameof(Prestamos), new { id = prestamo.Id_Trabajador });
        }

        [HttpGet]
        public async Task<IActionResult> HorasExtra(Guid id)
        {
            var trabajador = await _trabajadorService.Obtener(id);
            if (trabajador == null) return NotFound();

            var registros = await _horasExtraService.Obtener(id);

            ViewBag.NombreTrabajador = trabajador.Nombre_Completo;
            ViewBag.IdTrabajador = id;

            return View(registros);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarHorasExtra(HorasExtraRequest horasExtra)
        {
            var (ok, error) = await _horasExtraService.Registrar(horasExtra);

            if (ok)
            {
                await _bitacoraService.Registrar(User, "RegistrarHorasExtra", $"Registró {horasExtra.Horas} horas adicionales para el trabajador {horasExtra.Id_Trabajador}.");
                TempData["HorasExtraOk"] = "Horas adicionales registradas correctamente.";
            }
            else
            {
                TempData["HorasExtraError"] = error ?? "No se pudo registrar las horas adicionales.";
            }

            return RedirectToAction(nameof(HorasExtra), new { id = horasExtra.Id_Trabajador });
        }

        [HttpGet]
        public async Task<IActionResult> GenerarDetallePago(Guid id, bool modal = false)
        {
            var trabajador = await _trabajadorService.Obtener(id);
            if (trabajador == null) return NotFound();

            ViewBag.NombreTrabajador = trabajador.Nombre_Completo;
            ViewBag.IdTrabajador = id;
            ViewBag.Modal = modal;

            var hoy = DateOnly.FromDateTime(DateTime.Today);
            var inicioMes = new DateOnly(hoy.Year, hoy.Month, 1);

            return View(new Abstracciones.Modelos.Planilla.Planilla.GenerarDetallePagoRequest
            {
                Id_Trabajador = id,
                Fecha_Inicio = inicioMes,
                Fecha_Fin = hoy
            });
        }

        [HttpPost]
        public async Task<IActionResult> GenerarDetallePago(Abstracciones.Modelos.Planilla.Planilla.GenerarDetallePagoRequest request, bool modal = false)
        {
            var (ok, idPlanilla, error) = await _planillaService.GenerarDetallePago(request);

            if (!ok)
            {
                var trabajador = await _trabajadorService.Obtener(request.Id_Trabajador);
                ViewBag.NombreTrabajador = trabajador?.Nombre_Completo;
                ViewBag.IdTrabajador = request.Id_Trabajador;
                ViewBag.Modal = modal;
                ViewBag.ErrorApi = string.IsNullOrWhiteSpace(error)
                    ? "No se pudo generar el detalle de pago."
                    : error;
                return View(request);
            }

            await _bitacoraService.Registrar(User, "GenerarDetallePago", $"Generó el detalle de pago del período '{request.Periodo}' para el trabajador {request.Id_Trabajador}.");

            if (modal)
            {
                return Content($@"
            <script>
                window.parent.postMessage('crud-success', '*');
                window.open('/Planilla/Detalle/{idPlanilla}', '_blank');
            </script>", "text/html");
            }

            return RedirectToAction("Detalle", "Planilla", new { id = idPlanilla });
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
