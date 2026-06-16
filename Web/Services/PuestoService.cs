using static Abstracciones.Modelos.Puesto.Puesto;

namespace Web.Services
{
    public class PuestoService(HttpClient httpClient)
        : CrudService<PuestoResponse, PuestoRequest>(httpClient)
    {
        protected override string Ruta => "Puesto";
    }
}
