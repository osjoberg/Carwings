using Carwings.ApiClient.Models;

namespace Carwings.ApiClient.Results
{
    public class LastClimateStatusResult : IResult
    {
        internal LastClimateStatusResult()
        {
        }

        public RemoteACRecordsModel RemoteACRecords { get; set; }

        public int Status { get; set; }
    }
}
