using System;

namespace Carwings.ApiClient.Models
{
    public class PriceSimulatorDetailInfoDateModel
    {
        internal PriceSimulatorDetailInfoDateModel()
        {
        }

        public DateTime TargetDate { get; set; }

        public PriceSimulatorDetailInfoTripListModel PriceSimulatorDetailInfoTripList { get; set; }
    }
}