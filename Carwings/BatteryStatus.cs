using System;

using Carwings.ApiClient.Models;
using Carwings.ApiClient.Results;

namespace Carwings
{
    public class BatteryStatus
    {
        internal BatteryStatus(BatteryStatusResult batteryStatusResult)
        {
            Capacity = batteryStatusResult.BatteryCapacity;
            Remaining = batteryStatusResult.BatteryDegradation;
            RemainingPercent = (int)Math.Round((double)batteryStatusResult.BatteryDegradation / batteryStatusResult.BatteryCapacity * 100, MidpointRounding.AwayFromZero);
            EstimatedRangeKm = (int)Math.Round(batteryStatusResult.CruisingRangeAcOn / 1000D, MidpointRounding.AwayFromZero);
            TimeToFullCharge = TimeRequiredModel.ToTimeSpan(batteryStatusResult.TimeRequiredToFull);
            TimeToFullCharge3KW = TimeRequiredModel.ToTimeSpan(batteryStatusResult.TimeRequiredToFull200);
            TimeToFullCharge6KW = TimeRequiredModel.ToTimeSpan(batteryStatusResult.TimeRequiredToFull6kW);

            if (batteryStatusResult.PluginState == PluginState.NotConnected)
            {
                ChargingStatus = ChargingStatus.NotConnected;
            }
            else if (batteryStatusResult.ChargeMode == ChargeMode.NotCharging)
            {
                ChargingStatus = ChargingStatus.Connected;
            }
            else if (batteryStatusResult.ChargeMode == ChargeMode.Wall220v)
            {
                ChargingStatus = ChargingStatus.Charging;
            }
            else if (batteryStatusResult.ChargeMode == ChargeMode.RapidCharging)
            {
                ChargingStatus = ChargingStatus.RapidCharging;
            }

            UpdatedAt = batteryStatusResult.TimeStamp.ToLocalTime();
        }

        internal BatteryStatus(LastBatteryStatusResult lastBatteryStatusResult)
        {
            var batteryRecords = lastBatteryStatusResult.BatteryStatusRecords;
            var batteryStatus = batteryRecords.BatteryStatus;

            Capacity = batteryStatus.BatteryCapacity;
            Remaining = batteryStatus.BatteryRemainingAmount;
            RemainingPercent = (int)Math.Round((double)batteryStatus.BatteryRemainingAmount / batteryStatus.BatteryCapacity * 100, MidpointRounding.AwayFromZero);
            EstimatedRangeKm = (int)Math.Round(batteryRecords.CruisingRangeAcOn / 1000D, MidpointRounding.AwayFromZero);
            TimeToFullCharge = TimeRequiredModel.ToTimeSpan(batteryRecords.TimeRequiredToFull);
            TimeToFullCharge3KW = TimeRequiredModel.ToTimeSpan(batteryRecords.TimeRequiredToFull200);
            TimeToFullCharge6KW = TimeRequiredModel.ToTimeSpan(batteryRecords.TimeRequiredToFull6kW);

            if (batteryRecords.PluginState == PluginState.NotConnected)
            {
                ChargingStatus = ChargingStatus.NotConnected;
            }
            else if (batteryRecords.BatteryChargingStatus == ChargeMode.NotCharging)
            {
                ChargingStatus = ChargingStatus.Connected;
            }
            else if (batteryRecords.BatteryChargingStatus == ChargeMode.Wall220v)
            {
                ChargingStatus = ChargingStatus.Charging;
            }
            else if (batteryRecords.BatteryChargingStatus == ChargeMode.RapidCharging)
            {
                ChargingStatus = ChargingStatus.RapidCharging;
            }

            UpdatedAt = batteryRecords.NotificationDateAndTime.ToLocalTime();
        }

        public int Capacity { get; }

        public int Remaining { get; }

        public int RemainingPercent { get; }

        public int EstimatedRangeKm { get; }

        public ChargingStatus ChargingStatus { get; }

        public TimeSpan? TimeToFullCharge { get; }

        public TimeSpan? TimeToFullCharge3KW { get; }

        public TimeSpan? TimeToFullCharge6KW { get; }

        public DateTime UpdatedAt { get; set; }
    }
}
