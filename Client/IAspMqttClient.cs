using System.Threading.Tasks;

namespace Mqtt.Client.AspNetCore.Client
{
    public interface IAspMqttClient
    {
        Task StartClientAsync();
        Task StopClientAsync();
    }
}
