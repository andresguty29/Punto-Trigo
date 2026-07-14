using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.TiqueteDA;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Tiquete.Tiquete;

namespace DA.TiqueteDA
{
    public class TiqueteDA : ITiqueteDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public TiqueteDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> Agregar(TiqueteRequest tiquete, Guid? idTrabajador, string estadoInicial)
        {
            var idTiquete = Guid.NewGuid();
            var montoTotal = tiquete.Detalles.Sum(d => d.Cantidad * d.Precio_Unitario);

            var parametros = new DynamicParameters();
            parametros.Add("Id_Tiquete", idTiquete);
            parametros.Add("Id_Cliente", tiquete.Id_Cliente);
            parametros.Add("Id_Trabajador", idTrabajador);
            parametros.Add("Estado", estadoInicial);
            parametros.Add("Monto_Total", montoTotal);
            parametros.Add("Consecutivo", dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Output, size: 20);
            parametros.Add("Clave", dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Output, size: 50);

            await _sqlConnection.ExecuteAsync("Agregar_Tiquete", parametros);

            foreach (var detalle in tiquete.Detalles)
            {
                await _sqlConnection.ExecuteAsync("Agregar_Detalle_Tiquete", new
                {
                    Id_DetalleTiquete = Guid.NewGuid(),
                    Id_Tiquete = idTiquete,
                    detalle.Id_Producto,
                    detalle.Cantidad,
                    detalle.Precio_Unitario
                });
            }

            return idTiquete;
        }

        public async Task<Guid> ReintentarEnvio(Guid Id, string nuevoEstado)
        {
            await verificarTiqueteExiste(Id);
            await _sqlConnection.ExecuteAsync("Reintentar_Envio_Tiquete", new { Id_Tiquete = Id, Estado = nuevoEstado });
            return Id;
        }

        public async Task<IEnumerable<TiqueteResponse>> Obtener()
        {
            return await _sqlConnection.QueryAsync<TiqueteResponse>("Obtener_Tiquetes");
        }

        public async Task<TiqueteResponse> Obtener(Guid Id)
        {
            var resultado = await _sqlConnection.QueryAsync<TiqueteResponse>("Obtener_Tiquete", new { Id_Tiquete = Id });
            return resultado.FirstOrDefault();
        }

        public async Task<IEnumerable<DetalleTiqueteResponse>> ObtenerDetalle(Guid Id_Tiquete)
        {
            return await _sqlConnection.QueryAsync<DetalleTiqueteResponse>("Obtener_Detalle_Tiquete", new { Id_Tiquete });
        }

        private async Task verificarTiqueteExiste(Guid Id)
        {
            var resultado = await Obtener(Id);
            if (resultado == null)
                throw new Exception("No se encontro el tiquete.");
        }
    }
}
