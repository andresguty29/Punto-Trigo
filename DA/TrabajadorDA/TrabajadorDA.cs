using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.TrabajadorDA;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Trabajador.Trabajador;

namespace DA.TrabajadorDA
{
    public class TrabajadorDA : ITrabajadorDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public TrabajadorDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> Agregar(TrabajadorRequest trabajador)
        {
            string query = @"Agregar_Trabajador";

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id_Trabajador = Guid.NewGuid(),
                Cedula = trabajador.Cedula,
                Nombre_Completo = trabajador.Nombre_Completo,
                Id_Puesto = trabajador.Id_Puesto,
                Fecha_Ingreso = trabajador.Fecha_Ingreso.HasValue ? trabajador.Fecha_Ingreso.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null
            });

            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, TrabajadorRequest trabajador)
        {
            await verificarTrabajadorExiste(Id);

            string query = @"Editar_Trabajador";

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id_Trabajador = Id,
                Cedula = trabajador.Cedula,
                Nombre_Completo = trabajador.Nombre_Completo,
                Id_Puesto = trabajador.Id_Puesto,
                Fecha_Ingreso = trabajador.Fecha_Ingreso.HasValue ? trabajador.Fecha_Ingreso.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null
            });

            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarTrabajadorExiste(Id);

            string query = @"Eliminar_Trabajador";

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id_Trabajador = Id
            });

            return resultadoConsulta;
        }

        public async Task<IEnumerable<TrabajadorResponse>> Obtener()
        {
            string query = @"Obtener_Trabajadores";

            var resultadoConsulta = await _sqlConnection.QueryAsync<TrabajadorResponse>(query);

            return resultadoConsulta;
        }

        public async Task<TrabajadorResponse> Obtener(Guid Id)
        {
            string query = @"Obtener_Trabajador";

            var resultadoConsulta = await _sqlConnection.QueryAsync<TrabajadorResponse>(query, new
            {
                Id_Trabajador = Id
            });

            return resultadoConsulta.FirstOrDefault();
        }

        public async Task<Guid> Activar(Guid Id)
        {
            await verificarTrabajadorExiste(Id);
            var resultado = await _sqlConnection.ExecuteScalarAsync<Guid>("Activar_Trabajador", new { Id_Trabajador = Id });
            return resultado;
        }

        public async Task<IEnumerable<TrabajadorResponse>> ObtenerPanaderos()
        {
            return await _sqlConnection.QueryAsync<TrabajadorResponse>("Obtener_Trabajadores_Panaderos");
        }

        public async Task<Guid> ConfigurarPago(Guid Id, ConfigurarPagoRequest configuracion)
        {
            await verificarTrabajadorExiste(Id);

            return await _sqlConnection.ExecuteScalarAsync<Guid>("Configurar_Pago_Trabajador", new
            {
                Id_Trabajador = Id,
                configuracion.Tipo_Pago,
                configuracion.Salario_Base,
                configuracion.Tarifa_Hora
            });
        }

        public async Task<CalculoPagoResponse> CalcularPago(Guid Id, decimal? horasTrabajadas)
        {
            await verificarTrabajadorExiste(Id);

            var resultado = await _sqlConnection.QueryAsync<CalculoPagoResponse>("Calcular_Pago_Trabajador", new
            {
                Id_Trabajador = Id,
                Horas_Trabajadas = horasTrabajadas
            });

            return resultado.FirstOrDefault();
        }

        private async Task verificarTrabajadorExiste(Guid Id)
        {
            TrabajadorResponse? resultadoConsultaTrabajador = await Obtener(Id);

            if (resultadoConsultaTrabajador == null)
                throw new Exception("No se encontro trabajador");
        }

    }
}
