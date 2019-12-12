using Carwings.ApiClient.Models;

namespace Carwings
{
    public class Trip
    {
        internal Trip(PriceSimulatorDetailInfoTripModel trip)
        {
            DistanceKM = (double)trip.TravelDistance / 1000;
            TotalKW = trip.PowerConsumptTotal / 1000;
            EngineKW = trip.PowerConsumptMoter / 1000;
            RegenerativeKW = trip.PowerConsumptMinus / 1000;

            if (trip.TravelDistance != 0)
            {
                TotalKWPerKm = trip.PowerConsumptTotal / trip.TravelDistance;
                EngineKWPerKm = trip.PowerConsumptMoter / trip.TravelDistance;
                RegenerativeKWPerKm = trip.PowerConsumptMinus / trip.TravelDistance;
            }
        }

        public double DistanceKM { get; }

        public double TotalKW { get; }

        public double EngineKW { get; }

        public double RegenerativeKW { get; }

        public double TotalKWPerKm { get; }

        public double EngineKWPerKm { get; }

        public double RegenerativeKWPerKm { get; }
    }
}
