using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.UsuarioDA;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Usuario.Usuario;

namespace DA.UsuarioDA
{
    public class UsuarioDA : IUsuarioDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public UsuarioDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<IEnumerable<UsuarioResponse>> Obtener()
        {
            return await _sqlConnection.QueryAsync<UsuarioResponse>("Obtener_Usuarios");
        }

        public async Task<UsuarioResponse?> Obtener(Guid Id)
        {
            var resultado = await _sqlConnection.QueryAsync<UsuarioResponse>(
                "Obtener_Usuario", new { Id_Usuario = Id });
            return resultado.FirstOrDefault();
        }

        public async Task<UsuarioResponse?> ObtenerPorNombre(string nombreUsuario)
        {
            var resultado = await _sqlConnection.QueryAsync<UsuarioResponse>(
                "Login_Usuario", new { Nombre_Usuario = nombreUsuario });
            return resultado.FirstOrDefault();
        }

        public async Task<Guid> Agregar(UsuarioRequest usuario)
        {
            return await _sqlConnection.ExecuteScalarAsync<Guid>("Agregar_Usuario", new
            {
                Id_Usuario     = Guid.NewGuid(),
                usuario.Nombre_Usuario,
                usuario.Contrasena,
                usuario.Id_Trabajador,
                usuario.Rol
            });
        }

        public async Task<Guid> Editar(Guid Id, UsuarioRequest usuario)
        {
            await verificarUsuarioExiste(Id);
            return await _sqlConnection.ExecuteScalarAsync<Guid>("Editar_Usuario", new
            {
                Id_Usuario = Id,
                usuario.Nombre_Usuario,
                usuario.Contrasena,
                usuario.Id_Trabajador,
                usuario.Rol
            });
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarUsuarioExiste(Id);
            return await _sqlConnection.ExecuteScalarAsync<Guid>(
                "Eliminar_Usuario", new { Id_Usuario = Id });
        }

        public async Task<Guid> Activar(Guid Id)
        {
            await verificarUsuarioExiste(Id);
            return await _sqlConnection.ExecuteScalarAsync<Guid>(
                "Activar_Usuario", new { Id_Usuario = Id });
        }

        public async Task<Guid> CambiarContrasena(Guid Id, string contrasenaHash)
        {
            return await _sqlConnection.ExecuteScalarAsync<Guid>(
                "CambiarContrasena_Usuario", new { Id_Usuario = Id, Contrasena = contrasenaHash });
        }

        private async Task verificarUsuarioExiste(Guid Id)
        {
            var usuario = await Obtener(Id);
            if (usuario == null)
                throw new Exception("No se encontró el usuario.");
        }
    }
}
