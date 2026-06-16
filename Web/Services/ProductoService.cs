using static Abstracciones.Modelos.Producto.Producto;

namespace Web.Services
{
    public class ProductoService(HttpClient httpClient)
        : CrudService<ProductoResponse, ProductoRequest>(httpClient)
    {
        protected override string Ruta => "Producto";
    }
}
