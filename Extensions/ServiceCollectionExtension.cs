using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mqtt.Client.AspNetCore.Options;
using Mqtt.Client.AspNetCore.Services;
using Mqtt.Client.AspNetCore.Settings;
using MQTTnet.Client.Options;
using System;

namespace Mqtt.Client.AspNetCore.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMqttClientHostedService(this IServiceCollection services)
        {
            services.AddMqttClientServiceWithConfig(aspOptionBuilder =>
            {
                var clientSettinigs = AppSettingsProvider.ClientSettings;
                var brokerHostSettings = AppSettingsProvider.BrokerHostSettings;

                aspOptionBuilder
                .WithCredentials(clientSettinigs.UserName, clientSettinigs.Password)
                .WithClientId(clientSettinigs.Id)
                .WithTcpServer(brokerHostSettings.Host, brokerHostSettings.Port);
            });
            return services;
        }

        private static IServiceCollection AddMqttClientServiceWithConfig(this IServiceCollection services, Action<AspCoreMqttClientOptionBuilder> configure)
        {
            services.AddSingleton<IMqttClientOptions>(serviceProvider =>
            {
                var optionBuilder = new AspCoreMqttClientOptionBuilder(serviceProvider);
                configure(optionBuilder);
                return optionBuilder.Build();
            });
            services.AddSingleton<MqttClientService>();
            services.AddSingleton<IHostedService>(serviceProvider =>
            {
                return serviceProvider.GetService<MqttClientService>();
            });
            services.AddSingleton<MqttClientServiceProvider>(serviceProvider =>
            {
                var mqttClientService = serviceProvider.GetService<MqttClientService>();
                var mqttClientServiceProvider = new MqttClientServiceProvider(mqttClientService);
                return mqttClientServiceProvider;
            });
            return services;
        }
    }
}
