using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Dominio.Entities.Enums
{
    [JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumMemberConverter))]
    public enum TipoDefecto
    {
        [EnumMember(Value="Reproceso")]
        Reproceso,
        [EnumMember(Value = "Observado")]
        Observado
    }
}