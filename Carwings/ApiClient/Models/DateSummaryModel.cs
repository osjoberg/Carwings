namespace Carwings.ApiClient.Models
{
    public class DateSummaryModel
    {
        internal DateSummaryModel()
        {
        }

        public double ElectricMileage { get; set; }

        public int ElectricMileageLevel { get; set; }

        public double PowerConsumptMoter { get; set; }

        public int PowerConsumptMoterLevel { get; set; }

        public double PowerConsumptMinus { get; set; }

        public double PowerConsumptAux { get; set; }

        public int PowerConsumptAuxLevel { get; set; }

        public int PowerConsumptMinusLevel { get; set; }
    }
}
