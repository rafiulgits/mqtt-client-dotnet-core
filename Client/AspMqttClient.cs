using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System.Threading.Tasks;

namespace Mqtt.Client.AspNetCore.Client
{
    public class AspMqttClient : IAspMqttClient
    {
        private readonly IMqttClientOptions Options;

        private IMqttClient client;

        public AspMqttClient(IMqttClientOptions options)
        {
            Options = options;
            client = new MqttFactory().CreateMqttClient();
            client.UseApplicationMessageReceivedHandler(OnMessage);
        }

        public virtual void OnMessage(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            System.Console.WriteLine("A message is received");
        }

        public async Task StartClientAsync()
        {
            await client.ConnectAsync(Options);
            System.Console.WriteLine("Client is connected");
            await client.SubscribeAsync("hello/world");
            System.Console.WriteLine("Subscribed on a channel");
            if(!client.IsConnected)
            {
                await client.ReconnectAsync();
            }
        }

        public Task StopClientAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
