using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.ProductoDA;
using Abstracciones.Interfaces.DA.ProveedorDA;
using Abstracciones.Interfaces.Flujo.Producto;
using Abstracciones.Interfaces.Flujo.Proveedor;
using DA.ProductoDA;
using DA.ProveedorDA;
using DA.Repositorios;
using Flujo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductoFlujo, ProductoFlujo>();
builder.Services.AddScoped<IProductoDA, ProductoDA>();
builder.Services.AddScoped<IProveedorFlujo, ProveedorFlujo>();
builder.Services.AddScoped<IProveedorDA, ProveedorDA>();
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

app.Run();
