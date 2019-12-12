using System;

namespace Carwings.ApiClient.Models
{
    public class RemoteACRecordsModel
    {
        internal RemoteACRecordsModel()
        {
        }

        public RemoteACOperation RemoteACOperation { get; set; }

        public DateTime ACStartStopDateAndTime { get; set; }
    }
}
