using Carwings.ApiClient.Models;

namespace Carwings.ApiClient.Results
{
    public class LastBatteryStatusResult : IResult
    {
        internal LastBatteryStatusResult()
        {
        }

        public BatteryStatusRecordModel BatteryStatusRecords { get; set; }

        public int Status { get; set; }
    }
}