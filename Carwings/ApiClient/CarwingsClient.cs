using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;

using Carwings.ApiClient.Models;
using Carwings.ApiClient.Results;
using Newtonsoft.Json;

namespace Carwings.ApiClient
{
    public class CarwingsClient : IDisposable
    {
        internal readonly HttpClientWrapper HttpClient = new HttpClientWrapper();

        private const string BaseUrl = "https://gdcportalgw.its-mo.com/api_v190426_NE/gdc/";
        private const string InitialAppStr = "9s5rfKVuMrT03RtzajWNcA";

        public CarwingsClient()
        {
        }

        internal CarwingsClient(HttpClientWrapper httpClient)
        {
            HttpClient = httpClient;
        }

        public InitialResult Initialize()
        {
            var parameters = new Parameters
            {
                { "initial_app_str", InitialAppStr },
            };

            return InternalDispatch<InitialResult>("InitialApp_v2.php", parameters);
        }

        public LoginResult Login(string username, string password, Region region, string basePrm)
        {
            var encryptedPassword = new Blowfish().Encrypt(password, basePrm);

            var parameters = new Parameters
            {
                { "RegionCode", region.ToString() },
                { "UserId", username },
                { "Password", encryptedPassword },
                { "initial_app_str", InitialAppStr },
            };

            return InternalDispatch<LoginResult>("UserLoginRequest.php", parameters);
        }

        public AsyncResult BeginClimateOn(LoginResult loginResult, VehicleInfoModel vehicleInfo)
        {
            return InternalDispatch<AsyncResult>("ACRemoteRequest.php", loginResult, vehicleInfo);
        }

        public ClimateOnOffResult EndClimateOn(LoginResult loginResult, VehicleInfoModel vehicleInfo, AsyncResult asyncResult)
        {
            var parameters = new Parameters { { "resultKey", asyncResult.ResultKey } };

            return InternalDispatch<ClimateOnOffResult>("ACRemoteResult.php", loginResult, vehicleInfo, parameters);
        }

        public AsyncResult BeginClimateOff(LoginResult loginResult, VehicleInfoModel vehicleInfo)
        {
            return InternalDispatch<AsyncResult>("ACRemoteOffRequest.php", loginResult, vehicleInfo);
        }

        public ClimateOnOffResult EndClimateOff(LoginResult loginResult, VehicleInfoModel vehicleInfo, AsyncResult asyncResult)
        {
            var parameters = new Parameters { { "resultKey", asyncResult.ResultKey } };

            return InternalDispatch<ClimateOnOffResult>("ACRemoteOffResult.php", loginResult, vehicleInfo, parameters);
        }

        public ScheduleClimateOnResult ScheduleClimateOn(LoginResult loginResult, VehicleInfoModel vehicleInfo, DateTime startUtc)
        {
            var parameters = new Parameters
            {
                { "ExecuteTime", startUtc.ToUniversalTime().ToString("yyyy-MM-dd H:m") }
            };

            return InternalDispatch<ScheduleClimateOnResult>("ACRemoteUpdateRequest.php", loginResult, vehicleInfo, parameters); 
        }

        public CancelScheduleClimateOnResult RemoveScheduledClimateOn(LoginResult loginResult, VehicleInfoModel vehicleInfo)
        {
            return InternalDispatch<CancelScheduleClimateOnResult>("ACRemoteCancelRequest.php", loginResult, vehicleInfo);
        }

        public ScheduledClimateOnResult GetClimateOnSchedule(LoginResult loginResult, VehicleInfoModel vehicleInfo)
        {
            return InternalDispatch<ScheduledClimateOnResult>("GetScheduledACRemoteRequest.php", loginResult, vehicleInfo);
        }

        public LastClimateStatusResult GetLastClimateStatus(LoginResult loginResult, VehicleInfoModel vehicleInfo)
        {
            return InternalDispatch<LastClimateStatusResult>("RemoteACRecordsRequest.php", loginResult, vehicleInfo);
        }

        public StartChargingResult StartCharging(LoginResult loginResult, VehicleInfoModel vehicleInfo, DateTime start)
        {
            var parameters = new Parameters
            {
                { "ExecuteTime", start.ToString("yyyy-MM-dd H:m") }
            };

            return InternalDispatch<StartChargingResult>("BatteryRemoteChargingRequest.php", loginResult, vehicleInfo, parameters);
        }

        public AsyncResult BeginGetBatteryStatus(LoginResult loginResult, VehicleInfoModel vehicleInfo)
        {
            return InternalDispatch<AsyncResult>("BatteryStatusCheckRequest.php", loginResult, vehicleInfo);
        }

        public BatteryStatusResult EndGetBatteryStatus(LoginResult loginResult, VehicleInfoModel vehicleInfo, AsyncResult asyncResult)
        {
            var parameters = new Parameters { { "resultKey", asyncResult.ResultKey } };

            return InternalDispatch<BatteryStatusResult>("BatteryStatusCheckResultRequest.php", loginResult, vehicleInfo, parameters);
        }

        public LastBatteryStatusResult GetLastBatteryStatus(LoginResult loginResult, VehicleInfoModel vehicleInfo)
        {
            var result = InternalDispatch<LastBatteryStatusResult>("BatteryStatusRecordsRequest.php", loginResult, vehicleInfo);

            result.BatteryStatusRecords.TimeRequiredToFull = result.BatteryStatusRecords.TimeRequiredToFull ?? new TimeRequiredModel();
            result.BatteryStatusRecords.TimeRequiredToFull200 = result.BatteryStatusRecords.TimeRequiredToFull200 ?? new TimeRequiredModel();
            result.BatteryStatusRecords.TimeRequiredToFull6kW = result.BatteryStatusRecords.TimeRequiredToFull6kW ?? new TimeRequiredModel();

            return result;
        }

        public TodaysStatisticsResult GetTodaysStatistics(LoginResult loginResult, VehicleInfoModel vehicleInfo)
        {
            return InternalDispatch<TodaysStatisticsResult>("DriveAnalysisBasicScreenRequestEx.php", loginResult, vehicleInfo);
        }

        public MonthlyStatisticsResult GetMonthlyStatistics(LoginResult loginResult, VehicleInfoModel vehicleInfo, int year, int month)
        {
            var parameters = new Parameters { { "TargetMonth", $"{year:0000}{month:00}" }, { "tz", loginResult.CustomerInfo.Timezone } };

            return InternalDispatch<MonthlyStatisticsResult>("PriceSimulatorDetailInfoRequest.php", loginResult, vehicleInfo, parameters);
        }

        public void Dispose()
        {
            HttpClient?.Dispose();
        }

        private T InternalDispatch<T>(string endpoint, LoginResult loginResult, VehicleInfoModel vehicleInfo, Parameters parameters = null) where T : IResult
        {
            var concat = new Parameters
            {
                { "RegionCode", loginResult.CustomerInfo.RegionCode },
                { "VIN", vehicleInfo.Vin },
                { "custom_sessionid", vehicleInfo.CustomSessionId }
            };

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    concat.Add(parameter.Key, parameter.Value);
                }
            }

            return InternalDispatch<T>(endpoint, concat);
        }

        private T InternalDispatch<T>(string endpoint, Parameters parameters) where T : IResult
        {
            Trace.WriteLine($"Invoking Carwings API: {endpoint}");
            Trace.WriteLine("");

            var parametersString = string.Join(", ", parameters.Select(parameter => $"{parameter.Key}: {parameter.Value}"));
            Trace.WriteLine($"Params: {{{parametersString}}}");
            Trace.WriteLine("");

            var formContent = new FormUrlEncodedContent(parameters);

            var stopwatch = Stopwatch.StartNew();
            var response = HttpClient.PostAsync(BaseUrl + endpoint, formContent).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            stopwatch.Stop();

            Trace.WriteLine($"Result: {responseContent}");
            Trace.WriteLine("");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new CarwingsException($"Unexpected HTTP response. HTTP Status Code: {(int)response.StatusCode}, Response: {responseContent}.");
            }

            var deserialized = JsonConvert.DeserializeObject<T>(responseContent);
            if (deserialized.Status == 200 || deserialized.Status == 401)
            {
                return deserialized;
            }

            throw new CarwingsException($"Unexpected Service Response. HTTP Status Code: {(int)response.StatusCode}, Service status code: {deserialized.Status}.3");
        }
    }
}
