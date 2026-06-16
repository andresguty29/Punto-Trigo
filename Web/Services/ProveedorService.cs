using static Abstracciones.Modelos.Proveedor.Proveedor;

namespace Web.Services
{
    public class ProveedorService(HttpClient httpClient)
        : CrudService<ProveedorResponse, ProveedorRequest>(httpClient)
    {
        protected override string Ruta => "Proveedor";
    }
}
