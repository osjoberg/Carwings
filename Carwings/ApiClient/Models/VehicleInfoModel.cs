using Newtonsoft.Json;

namespace Carwings.ApiClient.Models
{
    public class VehicleInfoModel
    {
        internal VehicleInfoModel()
        {
        }

        public string Vin { get; set; }

        public string Nickname { get; set; }

        [JsonProperty("custom_sessionid")]
        public string CustomSessionId { get; set; }
    }
}