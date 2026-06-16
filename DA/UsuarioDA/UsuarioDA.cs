using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.UsuarioDA;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Usuario.Usuario;

namespace DA.UsuarioDA
{
    public class UsuarioDA : IUsuarioDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public UsuarioDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> Agregar(UsuarioRequest usuario)
        {
            string query = @"Agregar_Usuario";

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id_Usuario = Guid.NewGuid(),
                Nombre_Usuario = usuario.Nombre_Usuario,
                Contrasena = usuario.Contrasena,
                Id_Trabajador = usuario.Id_Trabajador
            });

            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, UsuarioRequest usuario)
        {
            await verificarUsuarioExiste(Id);

            string query = @"Editar_Usuario";

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id_Usuario = Id,
                Nombre_Usuario = usuario.Nombre_Usuario,
                Contrasena = usuario.Contrasena,
                Id_Trabajador = usuario.Id_Trabajador
            });

            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarUsuarioExiste(Id);

            string query = @"Eliminar_Usuario";

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id_Usuario = Id
            });

            return resultadoConsulta;
        }

        public async Task<IEnumerable<UsuarioResponse>> Obtener()
        {
            string query = @"Obtener_Usuarios";

            var resultadoConsulta = await _sqlConnection.QueryAsync<UsuarioResponse>(query);

            return resultadoConsulta;
        }

        public async Task<UsuarioResponse> Obtener(Guid Id)
        {
            string query = @"Obtener_Usuario";

            var resultadoConsulta = await _sqlConnection.QueryAsync<UsuarioResponse>(query, new
            {
                Id_Usuario = Id
            });

            return resultadoConsulta.FirstOrDefault();
        }

        public async Task<Guid> Activar(Guid Id)
        {
            await verificarUsuarioExiste(Id);
            var resultado = await _sqlConnection.ExecuteScalarAsync<Guid>("Activar_Usuario", new { Id_Usuario = Id });
            return resultado;
        }

        private async Task verificarUsuarioExiste(Guid Id)
        {
            UsuarioResponse? resultadoConsultaUsuario = await Obtener(Id);

            if (resultadoConsultaUsuario == null)
                throw new Exception("No se encontró el usuario");
        }

    }
}
