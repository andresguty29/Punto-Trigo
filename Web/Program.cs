using Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<ProductoService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:21153/");
});

builder.Services.AddHttpClient<ProveedorService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:21153/");
});

builder.Services.AddHttpClient<PuestoService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:21153/");
});

builder.Services.AddHttpClient<TrabajadorService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:21153/");
});

builder.Services.AddHttpClient<UsuarioService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:21153/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
