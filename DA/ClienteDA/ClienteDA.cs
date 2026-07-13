using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.ClienteDA;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Cliente.Cliente;

namespace DA.ClienteDA
{
    public class ClienteDA : IClienteDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;
        public ClienteDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> Agregar(ClienteRequest cliente)
        {
            string query = @"Agregar_Cliente";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id_Cliente = Guid.NewGuid(),
                Cedula = cliente.Cedula,
                Nombre_Completo = cliente.Nombre_Completo,
                Correo_Cliente = cliente.Correo_Cliente,
                Telefono_Cliente = cliente.Telefono_Cliente
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, ClienteRequest cliente)
        {
            await verificarClienteExiste(Id);
            string query = @"Editar_Cliente";

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id_Cliente = Id,
                Cedula = cliente.Cedula,
                Nombre_Completo = cliente.Nombre_Completo,
                Correo_Cliente = cliente.Correo_Cliente,
                Telefono_Cliente = cliente.Telefono_Cliente
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarClienteExiste(Id);
            string query = @"Eliminar_Cliente";

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id_Cliente = Id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<ClienteResponse>> Obtener()
        {
            string query = @"Obtener_Clientes";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ClienteResponse>(query);
            return resultadoConsulta;
        }

        public async Task<ClienteResponse> Obtener(Guid Id)
        {
            string query = @"Obtener_Cliente";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ClienteResponse>(query, new
            {
                Id_Cliente = Id
            });
            return resultadoConsulta.FirstOrDefault();
        }

        public async Task<ClienteResponse?> ObtenerPorCedula(string cedula)
        {
            string query = @"Obtener_Cliente_Por_Cedula";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ClienteResponse>(query, new
            {
                Cedula = cedula
            });
            return resultadoConsulta.FirstOrDefault();
        }

        public async Task<Guid> Activar(Guid Id)
        {
            await verificarClienteExiste(Id);
            var resultado = await _sqlConnection.ExecuteScalarAsync<Guid>("Activar_Cliente", new { Id_Cliente = Id });
            return resultado;
        }

        private async Task verificarClienteExiste(Guid Id)
        {
            ClienteResponse? resultadoConsultaCliente = await Obtener(Id);
            if (resultadoConsultaCliente == null)
                throw new Exception("No se encontro cliente");
        }
    }
}
