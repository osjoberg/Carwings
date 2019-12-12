using Carwings.ApiClient.Models;

namespace Carwings.ApiClient.Results
{
    public class MonthlyStatisticsResult : IResult
    {
        internal MonthlyStatisticsResult()
        {
        }

        public PriceSimulatorDetailInfoResponsePersonalDataModel PriceSimulatorDetailInfoResponsePersonalData { get; set; }

        public int Status { get; set; }
    }
}
