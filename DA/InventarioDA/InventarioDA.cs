using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.InventarioDA;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Inventario.Inventario;
using static Abstracciones.Modelos.Inventario.Movimiento;

namespace DA.InventarioDA
{
    public class InventarioDA : IInventarioDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public InventarioDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<IEnumerable<InventarioResponse>> Obtener()
        {
            return await _sqlConnection.QueryAsync<InventarioResponse>("Obtener_Inventarios");
        }

        public async Task<InventarioResponse> Obtener(Guid Id)
        {
            var resultado = await _sqlConnection.QueryAsync<InventarioResponse>(
                "Obtener_Inventario", new { Id_Inventario = Id });
            return resultado.FirstOrDefault();
        }

        public async Task<Guid> Agregar(InventarioRequest inventario)
        {
            return await _sqlConnection.ExecuteScalarAsync<Guid>("Agregar_Inventario", new
            {
                Id_Inventario = Guid.NewGuid(),
                inventario.Nombre,
                inventario.Unidad,
                inventario.Stock_Minimo,
                inventario.Id_Proveedor
            });
        }

        public async Task<Guid> Editar(Guid Id, InventarioRequest inventario)
        {
            await verificarExiste(Id);
            return await _sqlConnection.ExecuteScalarAsync<Guid>("Editar_Inventario", new
            {
                Id_Inventario = Id,
                inventario.Nombre,
                inventario.Unidad,
                inventario.Stock_Minimo,
                inventario.Id_Proveedor
            });
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarExiste(Id);
            return await _sqlConnection.ExecuteScalarAsync<Guid>(
                "Eliminar_Inventario", new { Id_Inventario = Id });
        }

        public async Task<Guid> Activar(Guid Id)
        {
            await verificarExiste(Id);
            return await _sqlConnection.ExecuteScalarAsync<Guid>(
                "Activar_Inventario", new { Id_Inventario = Id });
        }

        public async Task<Guid> RegistrarMovimiento(MovimientoRequest movimiento)
        {
            return await _sqlConnection.ExecuteScalarAsync<Guid>("Registrar_Movimiento", new
            {
                Id_Movimiento = Guid.NewGuid(),
                movimiento.Id_Inventario,
                movimiento.Tipo,
                movimiento.Cantidad,
                movimiento.Motivo,
                movimiento.Id_Proveedor
            });
        }

        public async Task<IEnumerable<MovimientoResponse>> ObtenerMovimientos(Guid Id_Inventario)
        {
            return await _sqlConnection.QueryAsync<MovimientoResponse>(
                "Obtener_Movimientos", new { Id_Inventario });
        }

        private async Task verificarExiste(Guid Id)
        {
            var item = await Obtener(Id);
            if (item == null)
                throw new Exception("No se encontró el item de inventario.");
        }
    }
}
