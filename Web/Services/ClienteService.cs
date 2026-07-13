using static Abstracciones.Modelos.Cliente.Cliente;

namespace Web.Services
{
    public class ClienteService(HttpClient httpClient)
        : CrudService<ClienteResponse, ClienteRequest>(httpClient)
    {
        protected override string Ruta => "Cliente";
    }
}
