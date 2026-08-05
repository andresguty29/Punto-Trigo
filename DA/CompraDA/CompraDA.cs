using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.CompraDA;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Compra.Compra;

namespace DA.CompraDA
{
    public class CompraDA : ICompraDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public CompraDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> Agregar(CompraRequest compra)
        {
            var idCompra = Guid.NewGuid();

            var resultado = await _sqlConnection.ExecuteScalarAsync<Guid>("Agregar_Compra", new
            {
                Id_Compra = idCompra,
                compra.Id_Proveedor,
                compra.Numero_Factura,
                compra.Categoria,
                compra.Descripcion_Adicional,
                compra.Monto_Total
            });

            foreach (var detalle in compra.Detalles)
            {
                await _sqlConnection.ExecuteScalarAsync<Guid>("Agregar_Detalle_Compra", new
                {
                    Id_DetalleCompra = Guid.NewGuid(),
                    Id_Compra = idCompra,
                    detalle.Id_Inventario,
                    detalle.Cantidad,
                    detalle.Unidad_Ingresada,
                    detalle.Costo_Unitario,
                    detalle.Fecha_Vencimiento
                });
            }

            return resultado;
        }

        public async Task<Guid> Anular(Guid Id)
        {
            await verificarCompraExiste(Id);
            await _sqlConnection.ExecuteAsync("Anular_Compra", new { Id_Compra = Id });
            return Id;
        }

        public async Task<Guid> Reclasificar(Guid Id, ReclasificarCompraRequest reclasificacion)
        {
            await verificarCompraExiste(Id);
            return await _sqlConnection.ExecuteScalarAsync<Guid>("Reclasificar_Compra", new
            {
                Id_Compra = Id,
                reclasificacion.Categoria,
                reclasificacion.Descripcion_Adicional
            });
        }

        public async Task<IEnumerable<CompraResponse>> Obtener(DateOnly? fechaInicio, DateOnly? fechaFin, decimal? montoMinimo)
        {
            return await _sqlConnection.QueryAsync<CompraResponse>("Obtener_Compras", new
            {
                Fecha_Inicio = fechaInicio.HasValue ? fechaInicio.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null,
                Fecha_Fin = fechaFin.HasValue ? fechaFin.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null,
                Monto_Minimo = montoMinimo
            });
        }

        public async Task<CompraResponse> Obtener(Guid Id)
        {
            var resultado = await _sqlConnection.QueryAsync<CompraResponse>("Obtener_Compra", new { Id_Compra = Id });
            return resultado.FirstOrDefault();
        }

        public async Task<IEnumerable<DetalleCompraResponse>> ObtenerDetalle(Guid Id_Compra)
        {
            return await _sqlConnection.QueryAsync<DetalleCompraResponse>("Obtener_Detalle_Compra", new { Id_Compra });
        }

        private async Task verificarCompraExiste(Guid Id)
        {
            var resultado = await Obtener(Id);
            if (resultado == null)
                throw new Exception("No se encontro la compra.");
        }
    }
}
