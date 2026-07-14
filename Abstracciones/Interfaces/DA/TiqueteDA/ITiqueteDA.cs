using static Abstracciones.Modelos.Tiquete.Tiquete;

namespace Abstracciones.Interfaces.DA.TiqueteDA
{
    public interface ITiqueteDA
    {
        Task<IEnumerable<TiqueteResponse>> Obtener();
        Task<TiqueteResponse> Obtener(Guid Id);
        Task<IEnumerable<DetalleTiqueteResponse>> ObtenerDetalle(Guid Id_Tiquete);
        Task<Guid> Agregar(TiqueteRequest tiquete, Guid? idTrabajador, string estadoInicial);
        Task<Guid> ReintentarEnvio(Guid Id, string nuevoEstado);
    }
}
