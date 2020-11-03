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

            DistanceKm = Trips.Sum(trip => trip.DistanceKm);
            EngineKW = Trips.Sum(trip => trip.EngineKW);
            TotalKW = Trips.Sum(trip => trip.TotalKW);
            RegenerativeKW = Trips.Sum(trip => trip.RegenerativeKW);

            if (DistanceKm != 0)
            {
                TotalKWPerKm = TotalKW / DistanceKm;
                EngineKWPerKm = EngineKW / DistanceKm;
                RegenerativeKWPerKm = RegenerativeKW / DistanceKm;
            }
        }

        public double RegenerativeKW { get; }

        public double TotalKW { get; }

        public double EngineKW { get; }

        public double DistanceKm { get; }

        public DateTime Date { get; }

        public Trip[] Trips { get; }

        public double TotalKWPerKm { get; }

        public double EngineKWPerKm { get; }

        public double RegenerativeKWPerKm { get; }
    }
}
