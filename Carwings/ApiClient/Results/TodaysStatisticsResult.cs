using Carwings.ApiClient.Models;

namespace Carwings.ApiClient.Results
{
    public class TodaysStatisticsResult : IResult
    {
        internal TodaysStatisticsResult()
        {
        }

        public DriveAnalysisBasicScreenResponsePersonalDataModel DriveAnalysisBasicScreenResponsePersonalData { get; set; }

        public int Status { get; set; }
    }
}
