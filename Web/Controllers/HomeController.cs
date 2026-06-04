using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var model = new DashboardViewModel
        {
            Today = DateTime.Now,
            UserInitials = "PT",
            Modules =
            [
                new DashboardModuleViewModel
                {
                    Key = "usuarios",
                    Name = "Usuarios",
                    Tag = "Acceso y seguridad",
                    Description = "Gestión de cuentas del sistema para acceso operativo del personal.",
                    Status = "Activo",
                    Accent = "#C41E1E",
                    Points = ["Usuarios registrados", "Control de acceso", "Estado de cuenta"],
                    Metrics =
                    [
                        new DashboardMetricViewModel { Label = "Fuente", Value = "API", Trend = "/api/Usuario" },
                        new DashboardMetricViewModel { Label = "Datos", Value = "En vivo", Trend = "Lectura directa" },
                        new DashboardMetricViewModel { Label = "Uso", Value = "Operativo", Trend = "Gestión diaria" },
                    ],
                    SubViews = [new DashboardSubviewViewModel { Key = "directorio", Label = "Directorio" }],
                    Table = new DashboardTableViewModel
                    {
                        Title = "Directorio de usuarios",
                        Columns = ["Usuario", "Trabajador", "Estado"],
                        Rows = [],
                        EmptyMessage = "Sin usuarios registrados.",
                        SourceUrl = "https://localhost:44378/api/Usuario"
                    },
                    Activity = ["Consulta activa de usuarios", "Sincronizado con API de usuarios", "Listo para altas y mantenimiento"]
                },
                new DashboardModuleViewModel
                {
                    Key = "planilla",
                    Name = "Planilla",
                    Tag = "Recursos humanos",
                    Description = "Control de trabajadores para operación diaria y administración del personal.",
                    Status = "Activo",
                    Accent = "#0B3D6E",
                    Points = ["Trabajadores activos", "Puesto asociado", "Estado visible"],
                    Metrics =
                    [
                        new DashboardMetricViewModel { Label = "Fuente", Value = "API", Trend = "/api/Trabajador" },
                        new DashboardMetricViewModel { Label = "Cobertura", Value = "Planilla", Trend = "Personal" },
                        new DashboardMetricViewModel { Label = "Estado", Value = "Operando", Trend = "Actualizable" },
                    ],
                    SubViews = [new DashboardSubviewViewModel { Key = "empleados", Label = "Empleados" }],
                    Table = new DashboardTableViewModel
                    {
                        Title = "Empleados",
                        Columns = ["Cédula", "Nombre", "Puesto", "Estado"],
                        Rows = [],
                        EmptyMessage = "Sin trabajadores registrados.",
                        SourceUrl = "https://localhost:44378/api/Trabajador"
                    },
                    Activity = ["Consulta de trabajadores habilitada", "Puesto visible por cada registro", "Base lista para gestión de personal"]
                },
                new DashboardModuleViewModel
                {
                    Key = "puestos",
                    Name = "Puestos",
                    Tag = "Organización",
                    Description = "Catálogo de puestos para estructurar funciones y responsabilidades del equipo.",
                    Status = "Activo",
                    Accent = "#4A6C2F",
                    Points = ["Puestos disponibles", "Referencia para planilla", "Gestión centralizada"],
                    Metrics =
                    [
                        new DashboardMetricViewModel { Label = "Fuente", Value = "API", Trend = "/api/Puesto" },
                        new DashboardMetricViewModel { Label = "Tipo", Value = "Catálogo", Trend = "Operativo" },
                        new DashboardMetricViewModel { Label = "Estado", Value = "Disponible", Trend = "Administrable" },
                    ],
                    SubViews = [new DashboardSubviewViewModel { Key = "catalogo", Label = "Catálogo" }],
                    Table = new DashboardTableViewModel
                    {
                        Title = "Catálogo de puestos",
                        Columns = ["Puesto", "ID", "Estado"],
                        Rows = [],
                        EmptyMessage = "Sin puestos registrados.",
                        SourceUrl = "https://localhost:44378/api/Puesto"
                    },
                    Activity = ["Consulta de puestos activa", "Integrado con planilla", "Listo para altas y edición"]
                },
                new DashboardModuleViewModel
                {
                    Key = "productos",
                    Name = "Productos",
                    Tag = "Catálogo",
                    Description = "Inventario de productos con control de precio y stock.",
                    Status = "Activo",
                    Accent = "#D4920A",
                    Points = ["Inventario disponible", "Precio actualizado", "Stock visible"],
                    Metrics =
                    [
                        new DashboardMetricViewModel { Label = "Fuente", Value = "API", Trend = "/api/Producto" },
                        new DashboardMetricViewModel { Label = "Cobertura", Value = "Productos", Trend = "Inventario" },
                        new DashboardMetricViewModel { Label = "Estado", Value = "Operando", Trend = "Con actualización" },
                    ],
                    SubViews = [new DashboardSubviewViewModel { Key = "catalogo", Label = "Catálogo" }],
                    Table = new DashboardTableViewModel
                    {
                        Title = "Catálogo de productos",
                        Columns = ["Nombre", "Precio", "Stock", "Estado"],
                        Rows = [],
                        EmptyMessage = "Sin productos registrados.",
                        SourceUrl = "https://localhost:44378/api/Producto"
                    },
                    Activity = ["Consulta de productos activa", "Visualización de precio y stock", "Listo para mantener inventario"]
                },
                new DashboardModuleViewModel
                {
                    Key = "proveedores",
                    Name = "Proveedores",
                    Tag = "Abastecimiento",
                    Description = "Directorio de proveedores para abastecimiento y compras.",
                    Status = "Activo",
                    Accent = "#0B3D6E",
                    Points = ["Contacto centralizado", "Estado del proveedor", "Consulta rápida"],
                    Metrics =
                    [
                        new DashboardMetricViewModel { Label = "Fuente", Value = "API", Trend = "/api/Proveedor" },
                        new DashboardMetricViewModel { Label = "Cobertura", Value = "Proveedores", Trend = "Compras" },
                        new DashboardMetricViewModel { Label = "Estado", Value = "Operando", Trend = "Actualizable" },
                    ],
                    SubViews = [new DashboardSubviewViewModel { Key = "directorio", Label = "Directorio" }],
                    Table = new DashboardTableViewModel
                    {
                        Title = "Directorio de proveedores",
                        Columns = ["Nombre", "Teléfono", "Correo", "Estado"],
                        Rows = [],
                        EmptyMessage = "Sin proveedores registrados.",
                        SourceUrl = "https://localhost:44378/api/Proveedor"
                    },
                    Activity = ["Consulta de proveedores activa", "Datos de contacto visibles", "Listo para gestión de abastecimiento"]
                },
            ]
        };

        return View(model);
    }
}