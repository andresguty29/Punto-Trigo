using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.DA.ProveedorDA;
using Abstracciones.Modelos.Proveedor;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Abstracciones.Modelos.Producto.Producto;
using static Abstracciones.Modelos.Proveedor.Proveedor;

namespace DA.ProveedorDA
{
    public class ProveedorDA : IProveedorDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;
        public ProveedorDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> Agregar(Proveedor.ProveedorRequest proveedor)
        {
            string query = @"Agregar_Proveedor";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id_Proveedor = Guid.NewGuid(),
                Nombre_Proveedor = proveedor.Nombre_Proveedor,
                Telefono_Proveedor = proveedor.Telefono_Proveedor,
                Correo_Proveedor = proveedor.Correo_Proveedor,

            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, Proveedor.ProveedorRequest proveedor)
        {
            await verificarProveedorExiste(Id);
            string query = @"Editar_Proveedor";

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id_Proveedor = Id,
                Nombre_Proveedor = proveedor.Nombre_Proveedor,
                Telefono_Proveedor = proveedor.Telefono_Proveedor,
                Correo_Proveedor = proveedor.Correo_Proveedor,
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarProveedorExiste(Id);
            string query = @"Eliminar_Proveedor";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id_Proveedor = Id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<Proveedor.ProveedorResponse>> Obtener()
        {
            string query = @"Obtener_Proveedores";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ProveedorResponse>(query);
            return resultadoConsulta;
        }

        public async Task<Proveedor.ProveedorResponse> Obtener(Guid Id)
        {
            string query = @"Obtener_Proveedor";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ProveedorResponse>(query, new
            {
                Id_Proveedor = Id,
            });
            return resultadoConsulta.FirstOrDefault();
        }

        public async Task<Guid> Activar(Guid Id)
        {
            await verificarProveedorExiste(Id);
            var resultado = await _sqlConnection.ExecuteScalarAsync<Guid>("Activar_Proveedor", new { Id_Proveedor = Id });
            return resultado;
        }

        private async Task verificarProveedorExiste(Guid Id)
        {
            ProveedorResponse? resultadoConsultaProveedor = await Obtener(Id);
            if (resultadoConsultaProveedor == null)
                throw new Exception("No se encontro proveedor");
        }
    }
}
