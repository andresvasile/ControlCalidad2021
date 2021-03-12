using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Dominio.Entities.Enums
{
    [JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumMemberConverter))]
    public enum TipoEmpleado
    {
        [EnumMember(Value="Administrativo")]
        Administrativo,
        [EnumMember(Value= "SupervisorDeLinea")]
        SupervisorDeLinea,
        [EnumMember(Value="SupervisorDeCalidad")]
        SupervisorDeCalidad
    }
}