using static Abstracciones.Modelos.Compra.Compra;

namespace Abstracciones.Interfaces.Flujo.Compra
{
    public interface ICompraFlujo
    {
        Task<IEnumerable<CompraResponse>> Obtener(DateOnly? fechaInicio, DateOnly? fechaFin, decimal? montoMinimo);
        Task<CompraResponse> Obtener(Guid Id);
        Task<IEnumerable<DetalleCompraResponse>> ObtenerDetalle(Guid Id_Compra);
        Task<Guid> Agregar(CompraRequest compra);
        Task<Guid> Anular(Guid Id);
        Task<Guid> Reclasificar(Guid Id, ReclasificarCompraRequest reclasificacion);
    }
}
