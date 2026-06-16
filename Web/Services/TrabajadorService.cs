using static Abstracciones.Modelos.Trabajador.Trabajador;

namespace Web.Services
{
    public class TrabajadorService(HttpClient httpClient)
        : CrudService<TrabajadorResponse, TrabajadorRequest>(httpClient)
    {
        protected override string Ruta => "Trabajador";
    }
}
