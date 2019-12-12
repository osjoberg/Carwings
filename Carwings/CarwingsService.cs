using System;
using System.Linq;
using Carwings.ApiClient;
using Carwings.ApiClient.Results;

namespace Carwings
{
    public class CarwingsService : IDisposable
    {
        internal readonly CarwingsClient CarwingsClient = new CarwingsClient();

        public Vehicle[] Vehicles => (LoginResult ?? throw new InvalidOperationException("Not logged in.")).VehicleInfo
            .Select(vehicleInfo => new Vehicle(this, vehicleInfo))
            .ToArray();

        internal LoginResult LoginResult { get; set;  }

        public void Login(string username, string password, Region region)
        {
            var basePrm = CarwingsClient.Initialize().Baseprm;

            LoginResult = CarwingsClient.Login(username, password, region, basePrm);
        }

        public void Dispose()
        {
            CarwingsClient?.Dispose();
        }
    }
}
