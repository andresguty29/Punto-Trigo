using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.ProduccionDA;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Produccion.Produccion;

namespace DA.ProduccionDA
{
    public class ProduccionDA : IProduccionDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public ProduccionDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<IEnumerable<AsignacionResponse>> ObtenerAsignaciones()
        {
            return await _sqlConnection.QueryAsync<AsignacionResponse>("Obtener_Asignaciones_Produccion");
        }

        public async Task<AsignacionResponse> ObtenerAsignacion(Guid Id_Asignacion)
        {
            var resultado = await _sqlConnection.QueryAsync<AsignacionResponse>(
                "Obtener_Asignacion_Produccion", new { Id_Asignacion });
            return resultado.FirstOrDefault();
        }

        public async Task<Guid> AgregarAsignacion(AsignacionRequest asignacion)
        {
            var idAsignacion = Guid.NewGuid();

            var resultado = await _sqlConnection.ExecuteScalarAsync<Guid>("Agregar_Asignacion_Produccion", new
            {
                Id_Asignacion = idAsignacion,
                asignacion.Id_Trabajador,
                asignacion.Id_Producto,
                asignacion.Cantidad_Diaria
            });

            await GuardarMateriales(idAsignacion, asignacion.Materiales);

            return resultado;
        }

        public async Task<Guid> EditarAsignacion(Guid Id_Asignacion, AsignacionRequest asignacion)
        {
            await verificarExiste(Id_Asignacion);

            var resultado = await _sqlConnection.ExecuteScalarAsync<Guid>("Editar_Asignacion_Produccion", new
            {
                Id_Asignacion,
                asignacion.Id_Trabajador,
                asignacion.Id_Producto,
                asignacion.Cantidad_Diaria
            });

            await _sqlConnection.ExecuteAsync("Eliminar_Materiales_Por_Asignacion", new { Id_Asignacion });
            await GuardarMateriales(Id_Asignacion, asignacion.Materiales);

            return resultado;
        }

        private async Task GuardarMateriales(Guid Id_Asignacion, List<MaterialAsignacionRequest> materiales)
        {
            foreach (var material in materiales)
            {
                await _sqlConnection.ExecuteScalarAsync<Guid>("Agregar_Material_Asignacion", new
                {
                    Id_AsignacionMaterial = Guid.NewGuid(),
                    Id_Asignacion,
                    material.Id_Inventario,
                    material.Cantidad
                });
            }
        }

        public async Task<IEnumerable<MaterialAsignacionResponse>> ObtenerMateriales(Guid Id_Asignacion)
        {
            return await _sqlConnection.QueryAsync<MaterialAsignacionResponse>("Obtener_Materiales_Asignacion", new { Id_Asignacion });
        }

        public async Task<Guid> MarcarRealizada(Guid Id_Asignacion)
        {
            await verificarExiste(Id_Asignacion);
            return await _sqlConnection.ExecuteScalarAsync<Guid>("Marcar_Produccion_Realizada", new { Id_Asignacion });
        }

        public async Task<Guid> EliminarAsignacion(Guid Id_Asignacion)
        {
            await verificarExiste(Id_Asignacion);
            return await _sqlConnection.ExecuteScalarAsync<Guid>("Eliminar_Asignacion_Produccion", new { Id_Asignacion });
        }

        public async Task<Guid> ActivarAsignacion(Guid Id_Asignacion)
        {
            await verificarExiste(Id_Asignacion);
            return await _sqlConnection.ExecuteScalarAsync<Guid>("Activar_Asignacion_Produccion", new { Id_Asignacion });
        }

        public async Task<IEnumerable<ListaProduccionResponse>> ObtenerListaDiaria(Guid? Id_Trabajador)
        {
            return await _sqlConnection.QueryAsync<ListaProduccionResponse>("Obtener_Lista_Produccion_Diaria", new
            {
                Id_Trabajador
            });
        }

        public async Task<IEnumerable<ProductoAsignadoResponse>> ObtenerProductosAsignados(Guid Id_Trabajador)
        {
            return await _sqlConnection.QueryAsync<ProductoAsignadoResponse>("Obtener_Productos_Asignados_Trabajador", new
            {
                Id_Trabajador
            });
        }

        private async Task verificarExiste(Guid Id_Asignacion)
        {
            var resultado = await ObtenerAsignacion(Id_Asignacion);
            if (resultado == null)
                throw new Exception("No se encontro la asignacion de produccion.");
        }
    }
}
