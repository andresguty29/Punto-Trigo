using static Abstracciones.Modelos.Perdida.Perdida;

namespace Abstracciones.Interfaces.Flujo.Perdida
{
    public interface IPerdidaFlujo
    {
        Task<IEnumerable<VencimientoPendienteResponse>> ObtenerPendientes();
        Task<ProcesarPerdidaResponse> Procesar(Guid idMovimiento);
    }
}
