using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Carwings.ApiClient.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ChargeMode
    {
        [EnumMember(Value = "NOT_CHARGING")]
        NotCharging,

        [EnumMember(Value = "220V")]
        Wall220v,

        [EnumMember(Value = "RAPIDLY_CHARGING")]
        RapidCharging,
    }
}
