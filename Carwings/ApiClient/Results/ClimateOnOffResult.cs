using System;

namespace Carwings.ApiClient.Results
{
    public class ClimateOnOffResult : IResult, IResponseFlag
    {
        internal ClimateOnOffResult()
        {
        }

        public int ResponseFlag { get; set; }

        public DateTime TimeStamp { get; set; }

        public string HvacStatus { get; set; }

        public int Status { get; set; }
    }
}