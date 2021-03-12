using System.Runtime.Serialization;

namespace Dominio.Entities.Enums
{
   
    public enum EstadoOrden
    {
        [EnumMember(Value="Activa")]
        Activa,
        [EnumMember(Value = "Pausada")]
        Pausada,
        [EnumMember(Value = "Finalizada")]
        Finalizada
    }
}