using System;
using System.Linq;

using Carwings.ApiClient.Models;

namespace Carwings
{
    public class DailyStatistics
    {
        internal DailyStatistics(PriceSimulatorDetailInfoDateModel priceSimulatorDetailInfoDate)
        {
            Date = priceSimulatorDetailInfoDate.TargetDate;
            Trips = priceSimulatorDetailInfoDate.PriceSimulatorDetailInfoTripList.PriceSimulatorDetailInfoTrip
                .Select(trip => new Trip(trip))
                .ToArray();

            DistanceKM = Trips.Sum(trip => trip.DistanceKM);
            EngineKW = Trips.Sum(trip => trip.EngineKW);
            TotalKW = Trips.Sum(trip => trip.TotalKW);
            RegenerativeKW = Trips.Sum(trip => trip.RegenerativeKW);

            if (DistanceKM != 0)
            {
                TotalKWPerKm = TotalKW / DistanceKM;
                EngineKWPerKm = EngineKW / DistanceKM;
                RegenerativeKWPerKm = RegenerativeKW / DistanceKM;
            }
        }

        public double RegenerativeKW { get; }

        public double TotalKW { get; }

        public double EngineKW { get; }

        public double DistanceKM { get; }

        public DateTime Date { get; }

        public Trip[] Trips { get; }

        public double TotalKWPerKm { get; }

        public double EngineKWPerKm { get; }

        public double RegenerativeKWPerKm { get; }
    }
}
