using static Abstracciones.Modelos.Cliente.Cliente;

namespace Abstracciones.Interfaces.DA.ClienteDA
{
    public interface IClienteDA
    {
        Task<IEnumerable<ClienteResponse>> Obtener();
        Task<ClienteResponse> Obtener(Guid Id);
        Task<ClienteResponse?> ObtenerPorCedula(string cedula);
        Task<Guid> Agregar(ClienteRequest cliente);
        Task<Guid> Editar(Guid Id, ClienteRequest cliente);
        Task<Guid> Eliminar(Guid Id);
        Task<Guid> Activar(Guid Id);
    }
}
