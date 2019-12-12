using System;
using System.Threading;
using Carwings.ApiClient;
using Carwings.ApiClient.Models;
using Carwings.ApiClient.Results;

namespace Carwings
{
    public class Vehicle
    {
        private readonly CarwingsService carwingsService;
        private readonly VehicleInfoModel vehicleInfo;

        internal Vehicle(CarwingsService carwingsService, VehicleInfoModel vehicleInfo)
        {
            this.carwingsService = carwingsService;
            this.vehicleInfo = vehicleInfo;
        }

        private CarwingsClient CarwingsClient => carwingsService.CarwingsClient;

        private LoginResult LoginResult => carwingsService.LoginResult;

        public BatteryStatus GetBatteryStatus()
        {
            return new BatteryStatus(CallAsync(() => CarwingsClient.BeginGetBatteryStatus(LoginResult, vehicleInfo), asyncResult => CarwingsClient.EndGetBatteryStatus(LoginResult, vehicleInfo, asyncResult)));
        }

        public BatteryStatus GetLastBatteryStatus()
        {
            return new BatteryStatus(CarwingsClient.GetLastBatteryStatus(LoginResult, vehicleInfo));
        }

        public ClimateStatus GetLastClimateStatus()
        {
            return new ClimateStatus(CarwingsClient.GetLastClimateStatus(LoginResult, vehicleInfo));
        }

        public ClimateStatus ClimateOn()
        {
            return new ClimateStatus(CallAsync(() => CarwingsClient.BeginClimateOn(LoginResult, vehicleInfo), asyncResult => CarwingsClient.EndClimateOn(LoginResult, vehicleInfo, asyncResult)));
        }

        public ClimateStatus ClimateOff()
        {
            return new ClimateStatus(CallAsync(() => CarwingsClient.BeginClimateOff(LoginResult, vehicleInfo), asyncResult => CarwingsClient.EndClimateOff(LoginResult, vehicleInfo, asyncResult)));
        }

        public void ScheduleClimateOn(DateTime start)
        {
            CarwingsClient.ScheduleClimateOn(LoginResult, vehicleInfo, start.ToUniversalTime());
        }

        public void CancelScheduleClimateOn()
        {
            CarwingsClient.RemoveScheduledClimateOn(LoginResult, vehicleInfo);
        }

        public DateTime? GetClimateOnSchedule()
        {
            return CarwingsClient.GetClimateOnSchedule(LoginResult, vehicleInfo).ExecuteTime?.ToLocalTime();
        }

        public void StartCharging(DateTime start)
        {
            CarwingsClient.StartCharging(LoginResult, vehicleInfo, start);
        }

        public TodaysStatistics GetTodaysStatistics()
        {
            return new TodaysStatistics(CarwingsClient.GetTodaysStatistics(LoginResult, vehicleInfo));
        }

        public MonthlyStatistics GetMonthlyStatistics(int year, int month)
        {
            return new MonthlyStatistics(CarwingsClient.GetMonthlyStatistics(LoginResult, vehicleInfo, year, month));
        }

        private T CallAsync<T>(Func<AsyncResult> begin, Func<AsyncResult, T> end) where T : IResponseFlag
        {
            var asyncResult = begin();

            for (var attempt = 0; attempt < 15; attempt++)
            {
                var result = end(asyncResult);
                if (result.ResponseFlag == 1)
                {
                    return result;
                }

                Thread.Sleep(5000);
            }

            throw CarwingsException.Timeout;
        }
    }
}