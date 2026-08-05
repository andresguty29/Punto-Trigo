using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.AsistenciaDA;
using Abstracciones.Interfaces.DA.ClienteDA;
using Abstracciones.Interfaces.DA.HorasExtraDA;
using Abstracciones.Interfaces.DA.PerdidaDA;
using Abstracciones.Interfaces.DA.PlanillaDA;
using Abstracciones.Interfaces.DA.PrestamoDA;
using Abstracciones.Interfaces.DA.CompraDA;
using Abstracciones.Interfaces.DA.BitacoraDA;
using Abstracciones.Interfaces.DA.RegistroAccesoDA;
using Abstracciones.Interfaces.DA.ReporteDA;
using Abstracciones.Interfaces.DA.TiqueteDA;
using Abstracciones.Interfaces.DA.InventarioDA;
using Abstracciones.Interfaces.DA.ProductoDA;
using Abstracciones.Interfaces.DA.ProduccionDA;
using Abstracciones.Interfaces.DA.ProveedorDA;
using Abstracciones.Interfaces.DA.PuestoDA;
using Abstracciones.Interfaces.DA.TrabajadorDA;
using Abstracciones.Interfaces.DA.VacacionDA;
using Abstracciones.Interfaces.DA.UsuarioDA;
using Abstracciones.Interfaces.Flujo.Asistencia;
using Abstracciones.Interfaces.Flujo.Cliente;
using Abstracciones.Interfaces.Flujo.HorasExtra;
using Abstracciones.Interfaces.Flujo.Perdida;
using Abstracciones.Interfaces.Flujo.Planilla;
using Abstracciones.Interfaces.Flujo.Prestamo;
using Abstracciones.Interfaces.Flujo.Compra;
using Abstracciones.Interfaces.Flujo.Bitacora;
using Abstracciones.Interfaces.Flujo.RegistroAcceso;
using Abstracciones.Interfaces.Flujo.Reporte;
using Abstracciones.Interfaces.Flujo.Tiquete;
using Abstracciones.Interfaces.Flujo.Inventario;
using Abstracciones.Interfaces.Flujo.Producto;
using Abstracciones.Interfaces.Flujo.Produccion;
using Abstracciones.Interfaces.Flujo.Proveedor;
using Abstracciones.Interfaces.Flujo.Puesto;
using Abstracciones.Interfaces.Flujo.Trabajador;
using Abstracciones.Interfaces.Flujo.Vacacion;
using Abstracciones.Interfaces.Flujo.Usuario;
using DA.AsistenciaDA;
using DA.ClienteDA;
using DA.HorasExtraDA;
using DA.PerdidaDA;
using DA.PlanillaDA;
using DA.PrestamoDA;
using DA.CompraDA;
using DA.BitacoraDA;
using DA.RegistroAccesoDA;
using DA.ReporteDA;
using DA.TiqueteDA;
using DA.InventarioDA;
using DA.ProductoDA;
using DA.ProduccionDA;
using DA.ProveedorDA;
using DA.PuestoDA;
using DA.Repositorios;
using DA.TrabajadorDA;
using DA.VacacionDA;
using DA.UsuarioDA;
using Flujo;

DapperTypeHandlers.Registrar();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirWeb", policy =>
    {
        policy.WithOrigins("https://localhost:7181")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IInventarioFlujo, InventarioFlujo>();
builder.Services.AddScoped<IInventarioDA, InventarioDA>();
builder.Services.AddScoped<IProductoFlujo, ProductoFlujo>();
builder.Services.AddScoped<IProductoDA, ProductoDA>();
builder.Services.AddScoped<IProduccionFlujo, ProduccionFlujo>();
builder.Services.AddScoped<IProduccionDA, ProduccionDA>();
builder.Services.AddScoped<IProveedorFlujo, ProveedorFlujo>();
builder.Services.AddScoped<IProveedorDA, ProveedorDA>();
builder.Services.AddScoped<IPuestoFlujo, PuestoFlujo>();
builder.Services.AddScoped<IPuestoDA, PuestoDA>();
builder.Services.AddScoped<ITrabajadorFlujo, TrabajadorFlujo>();
builder.Services.AddScoped<ITrabajadorDA, TrabajadorDA>();
builder.Services.AddScoped<IUsuarioFlujo, UsuarioFlujo>();
builder.Services.AddScoped<IUsuarioDA, UsuarioDA>();
builder.Services.AddScoped<IClienteFlujo, ClienteFlujo>();
builder.Services.AddScoped<IClienteDA, ClienteDA>();
builder.Services.AddScoped<ICompraFlujo, CompraFlujo>();
builder.Services.AddScoped<ICompraDA, CompraDA>();
builder.Services.AddScoped<ITiqueteFlujo, TiqueteFlujo>();
builder.Services.AddScoped<ITiqueteDA, TiqueteDA>();
builder.Services.AddScoped<IReporteFlujo, ReporteFlujo>();
builder.Services.AddScoped<IReporteDA, ReporteDA>();
builder.Services.AddScoped<IRegistroAccesoFlujo, RegistroAccesoFlujo>();
builder.Services.AddScoped<IRegistroAccesoDA, RegistroAccesoDA>();
builder.Services.AddScoped<IBitacoraFlujo, BitacoraFlujo>();
builder.Services.AddScoped<IBitacoraDA, BitacoraDA>();
builder.Services.AddScoped<IVacacionFlujo, VacacionFlujo>();
builder.Services.AddScoped<IVacacionDA, VacacionDA>();
builder.Services.AddScoped<IAsistenciaFlujo, AsistenciaFlujo>();
builder.Services.AddScoped<IAsistenciaDA, AsistenciaDA>();
builder.Services.AddScoped<IPrestamoFlujo, PrestamoFlujo>();
builder.Services.AddScoped<IPrestamoDA, PrestamoDA>();
builder.Services.AddScoped<IHorasExtraFlujo, HorasExtraFlujo>();
builder.Services.AddScoped<IHorasExtraDA, HorasExtraDA>();
builder.Services.AddScoped<IPlanillaFlujo, PlanillaFlujo>();
builder.Services.AddScoped<IPlanillaDA, PlanillaDA>();
builder.Services.AddScoped<IPerdidaFlujo, PerdidaFlujo>();
builder.Services.AddScoped<IPerdidaDA, PerdidaDA>();
builder.Services.AddScoped<IRepositorioDapper,RepositorioDapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("PermitirWeb");
app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
