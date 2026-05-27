using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.ProductoDA;
using Abstracciones.Modelos.Producto;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Producto.Producto;

namespace DA.ProductoDA
{
    public class ProductoDA : IProductoDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;
        public ProductoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> Agregar(ProductoRequest producto)
        {
            string query = @"Agregar_Producto";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id_Producto = Guid.NewGuid(),
                Nombre_Producto = producto.Nombre_Producto,
                Precio_Venta = producto.Precio_Venta,
                Stock_Actual = producto.Stock_Actual,
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, ProductoRequest producto)
        {
            await verificarProductoExiste(Id);
            string query = @"Editar_Producto";

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id_Producto = Id,
                Nombre_Producto = producto.Nombre_Producto,
                Precio_Venta = producto.Precio_Venta,
                Stock_Actual = producto.Stock_Actual,
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarProductoExiste(Id);
            string query = @"Eliminar_Producto";

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                IdProducto = Id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<ProductoResponse>> Obtener()
        {
            string query = @"Obtener_Productos";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoResponse>(query);
            return resultadoConsulta;
        }

        public async Task<ProductoResponse> Obtener(Guid Id)
        {
            string query = @"Obtener_Producto";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoResponse>(query, new
            {
                Id_Producto = Id
            });
            return resultadoConsulta.FirstOrDefault();
        }

        private async Task verificarProductoExiste(Guid Id)
        {
            ProductoResponse? resultadoConsultaProducto = await Obtener(Id);
            if (resultadoConsultaProducto == null)
                throw new Exception("No se encontro producto");
        }
    }
}
