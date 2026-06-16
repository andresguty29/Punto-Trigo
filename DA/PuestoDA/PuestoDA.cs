using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.PuestoDA;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Puesto.Puesto;

namespace DA.PuestoDA
{
    public class PuestoDA : IPuestoDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public PuestoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> Agregar(PuestoRequest puesto)
        {
            string query = @"Agregar_Puesto";

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id_Puesto = Guid.NewGuid(),
                Nombre_Puesto = puesto.Nombre_Puesto
            });

            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, PuestoRequest puesto)
        {
            await verificarPuestoExiste(Id);

            string query = @"Editar_Puesto";

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id_Puesto = Id,
                Nombre_Puesto = puesto.Nombre_Puesto
            });

            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarPuestoExiste(Id);

            string query = @"Eliminar_Puesto";

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id_Puesto = Id
            });

            return resultadoConsulta;
        }

        public async Task<IEnumerable<PuestoResponse>> Obtener()
        {
            string query = @"Obtener_Puestos";

            var resultadoConsulta = await _sqlConnection.QueryAsync<PuestoResponse>(query);

            return resultadoConsulta;
        }

        public async Task<PuestoResponse> Obtener(Guid Id)
        {
            string query = @"Obtener_Puesto";

            var resultadoConsulta = await _sqlConnection.QueryAsync<PuestoResponse>(query, new
            {
                Id_Puesto = Id
            });

            return resultadoConsulta.FirstOrDefault();
        }

        public async Task<Guid> Activar(Guid Id)
        {
            await verificarPuestoExiste(Id);
            var resultado = await _sqlConnection.ExecuteScalarAsync<Guid>("Activar_Puesto", new { Id_Puesto = Id });
            return resultado;
        }

        private async Task verificarPuestoExiste(Guid Id)
        {
            PuestoResponse? resultadoConsultaPuesto = await Obtener(Id);

            if (resultadoConsultaPuesto == null)
                throw new Exception("No se encontro puesto");
        }
    }
}
