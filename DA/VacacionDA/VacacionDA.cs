using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.VacacionDA;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Vacacion.Vacacion;

namespace DA.VacacionDA
{
    public class VacacionDA : IVacacionDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public VacacionDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<IEnumerable<VacacionAsignadaResponse>> Asignar(Guid idTrabajador)
        {
            var resultado = await _sqlConnection.QueryAsync<VacacionAsignadaResponse>("Asignar_Vacaciones", new
            {
                Id_Trabajador = idTrabajador
            });

            var creadas = resultado.ToList();
            foreach (var creada in creadas)
            {
                creada.Id_Trabajador = idTrabajador;
                creada.Fecha_Asignacion = DateTime.Now;
            }
            return creadas;
        }

        public async Task<IEnumerable<VacacionAsignadaResponse>> Obtener(Guid idTrabajador)
        {
            return await _sqlConnection.QueryAsync<VacacionAsignadaResponse>("Obtener_Vacaciones_Trabajador", new
            {
                Id_Trabajador = idTrabajador
            });
        }
    }
}
