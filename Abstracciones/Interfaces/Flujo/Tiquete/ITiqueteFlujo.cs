using static Abstracciones.Modelos.Tiquete.Tiquete;

namespace Abstracciones.Interfaces.Flujo.Tiquete
{
    public interface ITiqueteFlujo
    {
        Task<IEnumerable<TiqueteResponse>> Obtener();
        Task<TiqueteResponse> Obtener(Guid Id);
        Task<IEnumerable<DetalleTiqueteResponse>> ObtenerDetalle(Guid Id_Tiquete);
        Task<Guid> Agregar(TiqueteRequest tiquete, Guid? idTrabajador);
        Task<Guid> ReintentarEnvio(Guid Id);
    }
}
