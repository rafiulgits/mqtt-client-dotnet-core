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





**Now do whatever you want to do with `MQTTClientService`!**