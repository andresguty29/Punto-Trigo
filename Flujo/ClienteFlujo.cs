using Abstracciones.Interfaces.DA.ClienteDA;
using Abstracciones.Interfaces.Flujo.Cliente;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Cliente.Cliente;

namespace Flujo
{
    public class ClienteFlujo : IClienteFlujo
    {
        private IClienteDA _clienteDA;

        public ClienteFlujo(IClienteDA clienteDA)
        {
            _clienteDA = clienteDA;
        }

        public async Task<Guid> Agregar(ClienteRequest cliente)
        {
            try
            {
                return await _clienteDA.Agregar(cliente);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new InvalidOperationException("Ya existe un cliente con esa cédula.");
            }
        }

        public async Task<Guid> Editar(Guid Id, ClienteRequest cliente)
        {
            try
            {
                return await _clienteDA.Editar(Id, cliente);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new InvalidOperationException("Ya existe un cliente con esa cédula.");
            }
        }

        public Task<Guid> Eliminar(Guid Id)
        {
            return _clienteDA.Eliminar(Id);
        }

        public Task<IEnumerable<ClienteResponse>> Obtener()
        {
            return _clienteDA.Obtener();
        }

        public Task<ClienteResponse> Obtener(Guid Id)
        {
            return _clienteDA.Obtener(Id);
        }

        public Task<ClienteResponse?> ObtenerPorCedula(string cedula)
        {
            return _clienteDA.ObtenerPorCedula(cedula);
        }

        public Task<Guid> Activar(Guid Id)
        {
            return _clienteDA.Activar(Id);
        }
    }
}
