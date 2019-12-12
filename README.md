# Carwings
Client for Carwings API to control climate control and get statistics from Leaf vehicles.

## Install via NuGet
To install Upgrader, run the following command in the Package Manager Console:

```cmd
PM> Install-Package Carwings
```

You can also view the package page on [Nuget](https://www.nuget.org/packages/Carwings/).

## Example usage high-level service

```c#
    using Carwings:
    
    .
    .
    .

    using var service = new CarwingsService();

    // TODO: Insert real username and password here.
    service.Login("username", "password", Region.Europe);
    
    // Assuming we only have one vehicle on this.
    var car = service.Vehicles.Single();

    // Turn climate on.
    car.ClimateOn();

```

## Example usage more low-level client:

```c#
    using Carwings.ApiClient:
    
    .
    .
    .

    using var client = new CarwingsClient();

    var initializeResult = client.Initialize();

    // TODO: Insert real username and password here.
    var loginResult = client.Login("username", "password", Region.Europe, initializeResult.Baseprm);
       
    // Assuming we only have one vehicle on this.
    var vehicleInfo = loginResult.VehicleInfo.Single();
    
    var asyncResult = client.BeginClimateOn(loginResult, vehicleInfo);
    for (var attempt = 0; attempt < 15; attempt++)
    {
      var result = client.EndClimateOn(loginResult, vehicleInfo, asyncResult);
      if (result.ResponseFlag == 1)
      {
          Console.WriteLine("Success!");
          return;
      }

      Thread.Sleep(5000);
    }
    
    throw new Exception("Timeout.");
```


## Troubleshooting
If the returned state is not recent, try to add some delays between operations, typically five seconds to make sure that their backend has finished synchronized its state.