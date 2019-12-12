using System;
using System.Linq;

using Carwings.ApiClient;
using Carwings.ApiClient.Models;
using Carwings.ApiClient.Results;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Carwings.Test
{
    [TestClass]
    public class CarwingsClientTest
    {
        private readonly LoginResult loginResult = new LoginResult
        {
            CustomerInfo = new CustomerInfoModel { RegionCode = "NE" },
            VehicleInfo = new []
            {
                new VehicleInfoModel
                {
                    CustomSessionId = "<custom-session-id>",
                    Vin = "<vin>"
                },
            }
        };

        [TestMethod]
        public void InitializeReturnsBasePrm()
        {
            using var carwingsClient = new CarwingsClient(new FakeHttpClientWrapper(Resources.InitializeResponse));

            var result = carwingsClient.Initialize();

            Assert.AreEqual("88dSp7wWnV3bvv9Z88zEwg", result.Baseprm);
        }

        [TestMethod]
        public void LoginReturnsLoginResult()
        {
            using var carwingsClient = new CarwingsClient(new FakeHttpClientWrapper(Resources.LoginResponse));

            var result = carwingsClient.Login("john.doe", "secret", Region.Europe, "88dSp7wWnV3bvv9Z88zEwg");

            Assert.AreEqual("NE", result.CustomerInfo.RegionCode);
            Assert.AreEqual("<nickname>", result.VehicleInfo.Single().Nickname);
            Assert.AreEqual("<vin>", result.VehicleInfo.Single().Vin);
            Assert.AreEqual("<custom-session-id>", result.VehicleInfo.Single().CustomSessionId);
        }


        [TestMethod]
        public void BeginGetBatteryStatusReturnsAsyncResult()
        {
            using var carwingsClient = new CarwingsClient(new FakeHttpClientWrapper(Resources.BeginGetBatteryStatusResponse));

            var result = carwingsClient.BeginGetBatteryStatus(loginResult, loginResult.VehicleInfo.Single());

            Assert.AreEqual("6vh9J5i5Ae14eCO92heRjYfEOafUFirm3zMnUUJ5YkZcNNCqUG", result.ResultKey);
        }

        [TestMethod]
        public void EndGetBatteryStatusReturnsResult()
        {
            using var carwingsClient = new CarwingsClient(new FakeHttpClientWrapper(Resources.EndGetBatteryStatusResponse));

            var result = carwingsClient.EndGetBatteryStatus(loginResult, loginResult.VehicleInfo.Single(), new AsyncResult { ResultKey = "6vh9J5i5Ae14eCO92heRjYfEOafUFirm3zMnUUJ5YkZcNNCqUG" });

            Assert.AreEqual(0, result.TimeRequiredToFull.HourRequiredToFull);
            Assert.AreEqual(0, result.TimeRequiredToFull.MinutesRequiredToFull);
            Assert.AreEqual(0, result.TimeRequiredToFull200.HourRequiredToFull);
            Assert.AreEqual(0, result.TimeRequiredToFull200.MinutesRequiredToFull);
            Assert.AreEqual(0, result.TimeRequiredToFull6kW.HourRequiredToFull);
            Assert.AreEqual(0, result.TimeRequiredToFull6kW.MinutesRequiredToFull);
            Assert.AreEqual(12, result.BatteryCapacity);
            Assert.AreEqual(12, result.BatteryDegradation);
            Assert.AreEqual(ChargeMode.NotCharging, result.ChargeMode);
            Assert.AreEqual(PluginState.Connected, result.PluginState);
            Assert.AreEqual(144272, result.CruisingRangeAcOff);
            Assert.AreEqual(111760, result.CruisingRangeAcOn);
            Assert.AreEqual(1, result.ResponseFlag);
            Assert.AreEqual(new DateTime(2019, 12, 03, 16, 16, 38), result.TimeStamp);
        }

        [TestMethod]
        public void GetLastBatteryStatusReturnsResult()
        {
            using var carwingsClient = new CarwingsClient(new FakeHttpClientWrapper(Resources.GetLastBatteryStatusResponse));

            var result = carwingsClient.GetLastBatteryStatus(loginResult, loginResult.VehicleInfo.Single());

            Assert.AreEqual(0, result.BatteryStatusRecords.TimeRequiredToFull.HourRequiredToFull);
            Assert.AreEqual(0, result.BatteryStatusRecords.TimeRequiredToFull.MinutesRequiredToFull);
            Assert.AreEqual(0, result.BatteryStatusRecords.TimeRequiredToFull200.HourRequiredToFull);
            Assert.AreEqual(0, result.BatteryStatusRecords.TimeRequiredToFull200.MinutesRequiredToFull);
            Assert.AreEqual(0, result.BatteryStatusRecords.TimeRequiredToFull6kW.HourRequiredToFull);
            Assert.AreEqual(0, result.BatteryStatusRecords.TimeRequiredToFull6kW.MinutesRequiredToFull);
            Assert.AreEqual(12, result.BatteryStatusRecords.BatteryStatus.BatteryCapacity);
            Assert.AreEqual(12, result.BatteryStatusRecords.BatteryStatus.BatteryRemainingAmount);
            Assert.AreEqual(ChargeMode.NotCharging, result.BatteryStatusRecords.BatteryChargingStatus);
            Assert.AreEqual(PluginState.Connected, result.BatteryStatusRecords.PluginState);
            Assert.AreEqual(144272, result.BatteryStatusRecords.CruisingRangeAcOff);
            Assert.AreEqual(111760, result.BatteryStatusRecords.CruisingRangeAcOn);
            Assert.AreEqual(new DateTime(2019, 12, 03, 16, 16, 00), result.BatteryStatusRecords.NotificationDateAndTime);    
        }
    }
}
