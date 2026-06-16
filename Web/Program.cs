using Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var apiBase = builder.Configuration["ApiSettings:BaseUrl"]
    ?? throw new InvalidOperationException("Falta ApiSettings:BaseUrl en appsettings.json");

void AddCrud<T>(IServiceCollection services) where T : class =>
    services.AddHttpClient<T>(c => c.BaseAddress = new Uri(apiBase));

AddCrud<UsuarioService>(builder.Services);
AddCrud<TrabajadorService>(builder.Services);
AddCrud<PuestoService>(builder.Services);
AddCrud<ProveedorService>(builder.Services);
AddCrud<ProductoService>(builder.Services);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
