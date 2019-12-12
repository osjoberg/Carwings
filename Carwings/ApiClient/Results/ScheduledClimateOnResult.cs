using System;

namespace Carwings.ApiClient.Results
{
    public class ScheduledClimateOnResult : IResult
    {
        internal ScheduledClimateOnResult()
        {
        }

        public DateTime? ExecuteTime { get; set; }

        public int Status { get; set; }
    }
}
