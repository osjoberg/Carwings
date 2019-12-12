using System;

using Carwings.ApiClient.Models;
using Newtonsoft.Json;

namespace Carwings.ApiClient.Results
{
    public class BatteryStatusResult : IResult, IResponseFlag
    {
        internal BatteryStatusResult()
        {
        }

        public int ResponseFlag { get; set; }

        public DateTime TimeStamp { get; set; }

        public double CruisingRangeAcOn { get; set; }

        public double CruisingRangeAcOff { get; set; }

        public ChargeMode ChargeMode { get; set; }

        public int BatteryDegradation { get; set; }

        public int BatteryCapacity { get; set; }

        [JsonProperty(PropertyName = "timeRequiredToFull")]
        public TimeRequiredModel TimeRequiredToFull { get; set; }

        [JsonProperty(PropertyName = "timeRequiredToFull200")]
        public TimeRequiredModel TimeRequiredToFull200 { get; set; }

        [JsonProperty(PropertyName = "timeRequiredToFull200_6kW")]
        public TimeRequiredModel TimeRequiredToFull6kW { get; set; }

        public PluginState PluginState { get; set; }

        public int Status { get; set; }
    }
}