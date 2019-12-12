using Carwings.ApiClient.Models;

namespace Carwings.ApiClient.Results
{
    public class LoginResult : IResult
    {
        internal LoginResult()
        {
        }

        public VehicleInfoModel[] VehicleInfo { get; set; }

        public CustomerInfoModel CustomerInfo { get; set; }

        public int Status { get; set; }
    }
}