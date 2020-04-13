using MQTTnet.Client.Options;
using System;

namespace Mqtt.Client.AspNetCore.Options
{
    public class AspCoreMqttClientOptionBuilder : MqttClientOptionsBuilder
    {
        public IServiceProvider ServiceProvider { get; }

        public AspCoreMqttClientOptionBuilder(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
