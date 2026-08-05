using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.PrestamoDA;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Prestamo.Prestamo;

namespace DA.PrestamoDA
{
    public class PrestamoDA : IPrestamoDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public PrestamoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> Registrar(PrestamoRequest prestamo)
        {
            var idPrestamo = Guid.NewGuid();

            await _sqlConnection.ExecuteAsync("Registrar_Prestamo", new
            {
                Id_Prestamo = idPrestamo,
                prestamo.Id_Trabajador,
                prestamo.Monto,
                prestamo.Fecha,
                prestamo.Descripcion
            });

            return idPrestamo;
        }

        public async Task<IEnumerable<PrestamoResponse>> Obtener(Guid idTrabajador)
        {
            return await _sqlConnection.QueryAsync<PrestamoResponse>("Obtener_Prestamos_Trabajador", new
            {
                Id_Trabajador = idTrabajador
            });
        }
    }
}
