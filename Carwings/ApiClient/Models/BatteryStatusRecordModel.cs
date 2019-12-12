using System;

using Newtonsoft.Json;

namespace Carwings.ApiClient.Models
{
    public class BatteryStatusRecordModel
    {
        internal BatteryStatusRecordModel()
        {
        }

        public BatteryStatusModel BatteryStatus { get; set; }

        [JsonProperty(PropertyName = "timeRequiredToFull")]
        public TimeRequiredModel TimeRequiredToFull { get; set; }

        [JsonProperty(PropertyName = "timeRequiredToFull200")]
        public TimeRequiredModel TimeRequiredToFull200 { get; set; }

        [JsonProperty(PropertyName = "timeRequiredToFull200_6kW")]
        public TimeRequiredModel TimeRequiredToFull6kW { get; set; }

        public PluginState PluginState { get; set; }

        public double CruisingRangeAcOn { get; set; }

        public double CruisingRangeAcOff { get; set; }

        public ChargeMode BatteryChargingStatus { get; set; }

        public DateTime NotificationDateAndTime { get; set; }
    }
}