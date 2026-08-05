using static Abstracciones.Modelos.Perdida.Perdida;

namespace Abstracciones.Interfaces.DA.PerdidaDA
{
    public interface IPerdidaDA
    {
        Task<IEnumerable<VencimientoPendienteResponse>> ObtenerPendientes();
        Task<ProcesarPerdidaResponse> Procesar(Guid idMovimiento);
    }
}
