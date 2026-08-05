using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.RegistroAccesoDA;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.RegistroAcceso.RegistroAcceso;

namespace DA.RegistroAccesoDA
{
    public class RegistroAccesoDA : IRegistroAccesoDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public RegistroAccesoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> Registrar(RegistrarAccesoRequest registro)
        {
            var idRegistro = Guid.NewGuid();

            await _sqlConnection.ExecuteAsync("Registrar_Acceso", new
            {
                Id_Registro = idRegistro,
                registro.Id_Usuario,
                registro.Nombre_Usuario,
                registro.Exitoso
            });

            return idRegistro;
        }

        public Task CerrarSesion(Guid idRegistro)
        {
            return _sqlConnection.ExecuteAsync("Cerrar_Sesion_Acceso", new { Id_Registro = idRegistro });
        }

        public async Task<IEnumerable<RegistroAccesoResponse>> Obtener(DateOnly? fechaInicio, DateOnly? fechaFin, string? nombreUsuario)
        {
            return await _sqlConnection.QueryAsync<RegistroAccesoResponse>("Obtener_Registros_Acceso", new
            {
                Fecha_Inicio = fechaInicio.HasValue ? fechaInicio.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null,
                Fecha_Fin = fechaFin.HasValue ? fechaFin.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null,
                Nombre_Usuario = nombreUsuario
            });
        }

        public async Task<RegistroAccesoResponse?> Obtener(Guid id)
        {
            var resultado = await _sqlConnection.QueryAsync<RegistroAccesoResponse>("Obtener_Registro_Acceso", new { Id_Registro = id });
            return resultado.FirstOrDefault();
        }
    }
}
