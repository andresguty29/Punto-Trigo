using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.ProductoDA;
using Abstracciones.Interfaces.DA.ProveedorDA;
using Abstracciones.Interfaces.DA.PuestoDA;
using Abstracciones.Interfaces.DA.TrabajadorDA;
using Abstracciones.Interfaces.DA.UsuarioDA;
using Abstracciones.Interfaces.Flujo.Producto;
using Abstracciones.Interfaces.Flujo.Proveedor;
using Abstracciones.Interfaces.Flujo.Puesto;
using Abstracciones.Interfaces.Flujo.Trabajador;
using Abstracciones.Interfaces.Flujo.Usuario;
using DA.ProductoDA;
using DA.ProveedorDA;
using DA.PuestoDA;
using DA.Repositorios;
using DA.TrabajadorDA;
using DA.UsuarioDA;
using Flujo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductoFlujo, ProductoFlujo>();
builder.Services.AddScoped<IProductoDA, ProductoDA>();
builder.Services.AddScoped<IProveedorFlujo, ProveedorFlujo>();
builder.Services.AddScoped<IProveedorDA, ProveedorDA>();
builder.Services.AddScoped<IPuestoFlujo, PuestoFlujo>();
builder.Services.AddScoped<IPuestoDA, PuestoDA>();
builder.Services.AddScoped<ITrabajadorFlujo, TrabajadorFlujo>();
builder.Services.AddScoped<ITrabajadorDA, TrabajadorDA>();
builder.Services.AddScoped<IUsuarioFlujo, UsuarioFlujo>();
builder.Services.AddScoped<IUsuarioDA, UsuarioDA>();
builder.Services.AddScoped<IRepositorioDapper,RepositorioDapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
