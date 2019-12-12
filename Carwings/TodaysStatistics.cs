using Carwings.ApiClient.Results;

namespace Carwings
{
    public class TodaysStatistics
    {
        internal TodaysStatistics(TodaysStatisticsResult todaysStatistics)
        {
            var dateSummary = todaysStatistics.DriveAnalysisBasicScreenResponsePersonalData.DateSummary;

            TotalKWPerKm = dateSummary.ElectricMileage / 100;
            TotalRating = dateSummary.ElectricMileageLevel;

            EngineKWPerKm = dateSummary.PowerConsumptMoter / 1000;
            EngineRating = dateSummary.PowerConsumptMoterLevel;

            OtherKWPerKm = dateSummary.PowerConsumptAux / 1000;
            OtherRating = dateSummary.PowerConsumptAuxLevel;

            RegenerativeKWPerKm = dateSummary.PowerConsumptMinus / 1000;
            RegenerativeRating = dateSummary.PowerConsumptMinusLevel;
        }

        public double TotalKWPerKm { get; }

        public int TotalRating { get; }

        public double EngineKWPerKm { get; }

        public int EngineRating { get; }

        public double OtherKWPerKm { get; }

        public int OtherRating { get; }
        
        public double RegenerativeKWPerKm { get; }

        public int RegenerativeRating { get; }
    }
}
