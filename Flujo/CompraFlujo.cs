using Abstracciones.Interfaces.DA.CompraDA;
using Abstracciones.Interfaces.Flujo.Compra;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Compra.Compra;

namespace Flujo
{
    public class CompraFlujo : ICompraFlujo
    {
        private readonly ICompraDA _compraDA;

        public CompraFlujo(ICompraDA compraDA)
        {
            _compraDA = compraDA;
        }

        public async Task<Guid> Agregar(CompraRequest compra)
        {
            if (compra.Categoria == "Otro" && string.IsNullOrWhiteSpace(compra.Descripcion_Adicional))
                throw new InvalidOperationException("Debe indicar una descripcion adicional para la categoria 'Otro'.");

            try
            {
                return await _compraDA.Agregar(compra);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new InvalidOperationException("Ya existe una factura registrada con ese numero para este proveedor.");
            }
            catch (SqlException ex) when (ex.Number == 50000)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Task<Guid> Anular(Guid Id)
        {
            return _compraDA.Anular(Id);
        }

        public Task<Guid> Reclasificar(Guid Id, ReclasificarCompraRequest reclasificacion)
        {
            if (reclasificacion.Categoria == "Otro" && string.IsNullOrWhiteSpace(reclasificacion.Descripcion_Adicional))
                throw new InvalidOperationException("Debe indicar una descripcion adicional para la categoria 'Otro'.");

            return _compraDA.Reclasificar(Id, reclasificacion);
        }

        public Task<IEnumerable<CompraResponse>> Obtener(DateOnly? fechaInicio, DateOnly? fechaFin, decimal? montoMinimo)
        {
            if (fechaInicio.HasValue && fechaFin.HasValue && fechaFin < fechaInicio)
                throw new InvalidOperationException("La fecha final no puede ser menor a la fecha inicial.");

            return _compraDA.Obtener(fechaInicio, fechaFin, montoMinimo);
        }

        public Task<CompraResponse> Obtener(Guid Id)
        {
            return _compraDA.Obtener(Id);
        }

        public Task<IEnumerable<DetalleCompraResponse>> ObtenerDetalle(Guid Id_Compra)
        {
            return _compraDA.ObtenerDetalle(Id_Compra);
        }
    }
}
