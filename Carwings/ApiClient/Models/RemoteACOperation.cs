using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Carwings.ApiClient.Models
{
    [JsonConverter(typeof(StringEnumConverter))]

    public enum RemoteACOperation
    {
        [EnumMember(Value = "STOP")]
        Stop,

        [EnumMember(Value = "START")]

        Start,
    }
}
