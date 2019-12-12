namespace Carwings.ApiClient.Models
{
    public class PriceSimulatorDetailInfoTripModel
    {
        internal PriceSimulatorDetailInfoTripModel()
        {
        }

        public double PowerConsumptTotal { get; set; }

        public double PowerConsumptMoter { get; set; }

        public double PowerConsumptMinus { get; set; }

        public int TravelDistance { get; set; }
    }
}