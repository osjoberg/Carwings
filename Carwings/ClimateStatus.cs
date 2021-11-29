using System;

using Carwings.ApiClient.Models;
using Carwings.ApiClient.Results;

namespace Carwings
{
    public class ClimateStatus
    {
        internal ClimateStatus(LastClimateStatusResult getLastClimateStatus)
        {
            UpdatedAt = getLastClimateStatus.RemoteACRecords.ACStartStopDateAndTime.ToLocalTime();
            Running = getLastClimateStatus.RemoteACRecords.RemoteACOperation == RemoteACOperation.Start;
        }

        internal ClimateStatus(ClimateOnOffResult getLastClimateStatus)
        {
            if (getLastClimateStatus == null)
            {
                UnknownStatus = true;
            }
            else
            {
                UpdatedAt = getLastClimateStatus.TimeStamp.ToLocalTime();
                Running = getLastClimateStatus.HvacStatus == "ON";
            }
        }

        public DateTime UpdatedAt { get; }

        public bool Running { get; }

        public bool UnknownStatus { get; }
    }
}
