using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Streetcode.Messaging.Interfaces.EventPublish;
using Streetcode.Messaging.Services.EventPublish.MassTransit;

namespace Streetcode.Messaging.Extensions
{
    public static class MassTransit
    {
        public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
        {
            services.AddMassTransit(config =>
            {
                config.SetKebabCaseEndpointNameFormatter();

                if (assembly != null)
                    config.AddConsumers(assembly);

                config.UsingAzureServiceBus((context, configurator) =>
                {
                    configurator.Host(configuration["MessageBroker:ConnectionString"]);
                    configurator.ConfigureEndpoints(context);
                });
            });

            services.AddScoped<IEventPublisher, MassTransitEventPublisher>();

            return services;
        }
    }
}
