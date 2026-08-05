using Microsoft.AspNetCore.Authentication.Cookies;
using Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath       = "/Login";
        options.AccessDeniedPath = "/Login/AccesoDenegado";
        options.ExpireTimeSpan  = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
    });

var apiBase = builder.Configuration["ApiSettings:BaseUrl"]
    ?? throw new InvalidOperationException("Falta ApiSettings:BaseUrl en appsettings.json");

void AddCrud<T>(IServiceCollection services) where T : class =>
    services.AddHttpClient<T>(c => c.BaseAddress = new Uri(apiBase));

AddCrud<InventarioService>(builder.Services);
AddCrud<UsuarioService>(builder.Services);
AddCrud<TrabajadorService>(builder.Services);
AddCrud<PuestoService>(builder.Services);
AddCrud<ProveedorService>(builder.Services);
AddCrud<ProductoService>(builder.Services);
AddCrud<ProduccionService>(builder.Services);
AddCrud<ClienteService>(builder.Services);
AddCrud<CompraService>(builder.Services);
AddCrud<TiqueteService>(builder.Services);
AddCrud<ReporteService>(builder.Services);
AddCrud<RegistroAccesoService>(builder.Services);
AddCrud<BitacoraService>(builder.Services);
AddCrud<VacacionService>(builder.Services);
AddCrud<AsistenciaService>(builder.Services);
AddCrud<PrestamoService>(builder.Services);
AddCrud<HorasExtraService>(builder.Services);
AddCrud<PlanillaService>(builder.Services);
AddCrud<PerdidaService>(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
