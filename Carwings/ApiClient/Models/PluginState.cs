using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Carwings.ApiClient.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PluginState
    {
        [EnumMember(Value = "NOT_CONNECTED")]
        NotConnected,

        [EnumMember(Value = "CONNECTED")]
        Connected
    }
}
