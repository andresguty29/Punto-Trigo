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
                    Accent = "#C41E1E",
                    Table = new DashboardTableViewModel
                    {
                        Title = "Directorio de usuarios",
                        Columns = ["Usuario", "Trabajador", "Estado"],
                        EmptyMessage = "Sin usuarios registrados.",
                        SourceUrl = "https://localhost:44378/api/Usuario"
                    }
                },
                new DashboardModuleViewModel
                {
                    Key = "planilla",
                    Name = "Planilla",
                    Tag = "Recursos humanos",
                    Description = "Control de trabajadores para operación diaria y administración del personal.",
                    Accent = "#0B3D6E",
                    Table = new DashboardTableViewModel
                    {
                        Title = "Empleados",
                        Columns = ["Cédula", "Nombre", "Puesto", "Estado"],
                        EmptyMessage = "Sin trabajadores registrados.",
                        SourceUrl = "https://localhost:44378/api/Trabajador"
                    }
                },
                new DashboardModuleViewModel
                {
                    Key = "puestos",
                    Name = "Puestos",
                    Tag = "Organización",
                    Description = "Catálogo de puestos para estructurar funciones y responsabilidades del equipo.",
                    Accent = "#4A6C2F",
                    Table = new DashboardTableViewModel
                    {
                        Title = "Catálogo de puestos",
                        Columns = ["Puesto", "Estado"],
                        EmptyMessage = "Sin puestos registrados.",
                        SourceUrl = "https://localhost:44378/api/Puesto"
                    }
                },
                new DashboardModuleViewModel
                {
                    Key = "productos",
                    Name = "Productos",
                    Tag = "Catálogo",
                    Description = "Inventario de productos con control de precio y stock.",
                    Accent = "#D4920A",
                    Table = new DashboardTableViewModel
                    {
                        Title = "Catálogo de productos",
                        Columns = ["Nombre", "Proveedor", "Precio", "Stock", "Estado"],
                        EmptyMessage = "Sin productos registrados.",
                        SourceUrl = "https://localhost:44378/api/Producto"
                    }
                },
                new DashboardModuleViewModel
                {
                    Key = "proveedores",
                    Name = "Proveedores",
                    Tag = "Abastecimiento",
                    Description = "Directorio de proveedores para abastecimiento y compras.",
                    Accent = "#0B3D6E",
                    Table = new DashboardTableViewModel
                    {
                        Title = "Directorio de proveedores",
                        Columns = ["Nombre", "Teléfono", "Correo", "Estado"],
                        EmptyMessage = "Sin proveedores registrados.",
                        SourceUrl = "https://localhost:44378/api/Proveedor"
                    }
                },
            ]
        };

        return View(model);
    }
}
