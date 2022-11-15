# MQTT Client ASP.NET Core

This repository is an example of how to use [MQTTnet](https://github.com/chkr1011/MQTTnet) client in ASP.NET core web application with services.



MQTT Client is running in `MqttClientService` by`IHostedService`, and it is a `singleton` service.

To access `MqttClientService` from other external services inject `MQTTClientServiceProvider` in service constructor.

Here is an example

```csharp
public class ExtarnalService
{
    private readonly IMqttClientService mqttClientService;
    public ExtarnalService(MqttClientServiceProvider provider)
    {
        mqttClientService = provider.MqttClientService;
    }
}
```



**Configuration**

Configure your MQTT settings in `appSettings.json`

```json
"BrokerHostSettings": {
    "Host": "localhost",
    "Port": 1883
  },

  "ClientSettings": {
    "Id": "5eb020f043ba8930506acbdd",
    "UserName": "rafiul",
    "Password": "12345678"
  },
```



**Reconnecting**

Reference Code：https://github.com/dotnet/MQTTnet/blob/master/Samples/Client/Client_Connection_Samples.cs
The implemented code is in `MqttClientService.cs`

1. Reconnect_Using_Event

   ```c#
   public async Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
   {
   
         if (e.ClientWasConnected)
         {
              // Use the current options as the new options.
              await mqttClient.ConnectAsync(mqttClient.Options);
         }
   }
   
   ```

   "Reconnect_Using_Event" is not recommended, so the code is commented out ,the following code is recommended "Reconnect_Using_Time"

2. Reconnect_Using_Timer

   ```c#
   public async Task StartAsync(CancellationToken cancellationToken)
   {
          await mqttClient.ConnectAsync(options);
          /* 
           * This sample shows how to reconnect when the connection was dropped.
           * This approach uses a custom Task/Thread which will monitor the connection status.
           * This is the recommended way but requires more custom code!
           */
           _ = Task.Run(
              async () =>
              {
                  // // User proper cancellation and no while(true).
                  while (true)
                  {
                      try
                      {
                          // This code will also do the very first connect! So no call to _ConnectAsync_ is required in the first place.
                          if (!await mqttClient.TryPingAsync())
                          {
                              await mqttClient.ConnectAsync(mqttClient.Options, CancellationToken.None);
   
                              // Subscribe to topics when session is clean etc.
                              _logger.LogInformation("The MQTT client is connected.");
                          }
                      }
                      catch (Exception ex)
                      {
                          // Handle the exception properly (logging etc.).
                          _logger.LogError(ex, "The MQTT client  connection failed");
                      }
                      finally
                      {
                          // Check the connection state every 5 seconds and perform a reconnect if required.
                          await Task.Delay(TimeSpan.FromSeconds(5));
                      }
                  }
              });
   
    }
   ```

   

**Now do whatever you want to do with `MQTTClientService`!**