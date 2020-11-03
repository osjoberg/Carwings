using System.Linq;

using Carwings.ApiClient.Results;

namespace Carwings
{
    public class MonthlyStatistics
    {
        internal MonthlyStatistics(MonthlyStatisticsResult monthlyStatistics)
        {
            var priceSimulatorDetailInfoResponsePersonalData = monthlyStatistics.PriceSimulatorDetailInfoResponsePersonalData;
            var priceSimulatorDetailInfoDate = priceSimulatorDetailInfoResponsePersonalData.PriceSimulatorDetailInfoDateList.PriceSimulatorDetailInfoDate;

            DailyStatistics = priceSimulatorDetailInfoResponsePersonalData.ExistFlg == "NOTEXIST" ? new DailyStatistics[] { } : priceSimulatorDetailInfoDate
                .Select(psdid => new DailyStatistics(psdid))
                .ToArray();

            DistanceKm = DailyStatistics.Sum(day => day.DistanceKm);
            EngineKW = DailyStatistics.Sum(day => day.EngineKW);
            TotalKW = DailyStatistics.Sum(day => day.TotalKW);
            RegenerativeKW = DailyStatistics.Sum(day => day.RegenerativeKW);

            if (DistanceKm != 0)
            {
                TotalKWPerKm = TotalKW / DistanceKm;
                EngineKWPerKm = EngineKW / DistanceKm;
                RegenerativeKWPerKm = RegenerativeKW / DistanceKm;
            }
        }

        public double RegenerativeKWPerKm { get; }

        public double EngineKWPerKm { get; }

        public double TotalKWPerKm { get; }

        public double RegenerativeKW { get; }

        public double TotalKW { get; }

        public double EngineKW { get; }

        public double DistanceKm { get; }

        public DailyStatistics[] DailyStatistics { get; }
    }
}
