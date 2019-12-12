using System;

namespace Carwings.ApiClient.Models
{
    public class TimeRequiredModel
    {
        internal TimeRequiredModel()
        {
        }

        public int HourRequiredToFull { get; set; }

        public int MinutesRequiredToFull { get; set; }

        internal static TimeSpan? ToTimeSpan(TimeRequiredModel timeRequired)
        {
            if (timeRequired == null)
            {
                return null;
            }

            if (timeRequired.MinutesRequiredToFull == 0 && timeRequired.HourRequiredToFull == 0)
            {
                return null;
            }

            return new TimeSpan(timeRequired.HourRequiredToFull, timeRequired.MinutesRequiredToFull, 0);
        }
    }
}