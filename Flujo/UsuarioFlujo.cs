using System.Security.Cryptography;
using Abstracciones.Interfaces.DA.UsuarioDA;
using Abstracciones.Interfaces.Flujo.Usuario;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Usuario.Usuario;

namespace Flujo
{
    public class UsuarioFlujo : IUsuarioFlujo
    {
        private readonly IUsuarioDA _usuarioDA;

        public UsuarioFlujo(IUsuarioDA usuarioDA)
        {
            _usuarioDA = usuarioDA;
        }

        public Task<IEnumerable<UsuarioResponse>> Obtener() => _usuarioDA.Obtener();

        public Task<UsuarioResponse?> Obtener(Guid Id) => _usuarioDA.Obtener(Id);

        public Task<Guid> Eliminar(Guid Id) => _usuarioDA.Eliminar(Id);

        public Task<Guid> Activar(Guid Id) => _usuarioDA.Activar(Id);

        public async Task<Guid> Agregar(UsuarioRequest usuario)
        {
            usuario.Contrasena = HashPassword(usuario.Contrasena!);
            try
            {
                return await _usuarioDA.Agregar(usuario);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new InvalidOperationException(ResolverMensajeUnicidad(ex.Message));
            }
        }

        public async Task<Guid> Editar(Guid Id, UsuarioRequest usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Contrasena))
            {
                var existente = await _usuarioDA.Obtener(Id);
                usuario.Contrasena = existente?.Contrasena;
            }
            else
            {
                usuario.Contrasena = HashPassword(usuario.Contrasena);
            }

            try
            {
                return await _usuarioDA.Editar(Id, usuario);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new InvalidOperationException(ResolverMensajeUnicidad(ex.Message));
            }
        }

        private static string ResolverMensajeUnicidad(string mensaje)
        {
            if (mensaje.Contains("UQ_Usuario_NombreUsuario"))
                return "El nombre de usuario ya está en uso.";
            if (mensaje.Contains("UQ_Usuario_Trabajador"))
                return "Este trabajador ya tiene un usuario asignado.";
            return "Ya existe un registro con esos datos.";
        }

        public async Task<LoginResponse?> Login(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre_Usuario) ||
                string.IsNullOrWhiteSpace(request.Contrasena))
                return null;

            var usuario = await _usuarioDA.ObtenerPorNombre(request.Nombre_Usuario);
            if (usuario == null) return null;

            if (!VerifyPassword(request.Contrasena, usuario.Contrasena!))
                return null;

            return new LoginResponse
            {
                Id_Usuario      = usuario.Id_Usuario,
                Nombre_Usuario  = usuario.Nombre_Usuario,
                Id_Trabajador   = usuario.Id_Trabajador,
                Nombre_Trabajador = usuario.Nombre_Trabajador,
                Rol             = usuario.Rol
            };
        }

        public async Task<bool> CambiarContrasena(Guid Id, CambiarContrasenaRequest request)
        {
            var usuario = await _usuarioDA.Obtener(Id);
            if (usuario == null) return false;

            if (!VerifyPassword(request.ContrasenaActual!, usuario.Contrasena!))
                return false;

            var nuevoHash = HashPassword(request.ContrasenaNueva!);
            await _usuarioDA.CambiarContrasena(Id, nuevoHash);
            return true;
        }

        // ── Hashing PBKDF2 ──────────────────────────────────
        private static string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);
            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }

        private static bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split(':');
            if (parts.Length != 2) return false;

            byte[] salt       = Convert.FromBase64String(parts[0]);
            byte[] storedBytes = Convert.FromBase64String(parts[1]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            return CryptographicOperations.FixedTimeEquals(hash, storedBytes);
        }
    }
}
