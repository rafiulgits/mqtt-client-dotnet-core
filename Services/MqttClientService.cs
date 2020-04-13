using Microsoft.Extensions.Hosting;
using Mqtt.Client.AspNetCore.Client;
using MQTTnet.Client.Options;
using System.Threading;
using System.Threading.Tasks;

namespace Mqtt.Client.AspNetCore.Services
{
    public class MqttClientService : IHostedService
    {
        private AspMqttClient Client;

        public MqttClientService(IMqttClientOptions options)
        {
            Client = new AspMqttClient(options);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Client.StartClientAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Client.StopClientAsync();
        }
    }
}
