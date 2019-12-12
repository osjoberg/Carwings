namespace Carwings.ApiClient.Models
{
    public class BatteryStatusModel
    {
        internal BatteryStatusModel()
        {
        }

        public int BatteryCapacity { get; set; }

        public int BatteryRemainingAmount { get; set; }
    }
}