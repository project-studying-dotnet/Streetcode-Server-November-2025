using MassTransit;
using Streetcode.Messaging.Interfaces.EventPublish;

namespace Streetcode.Messaging.Services.EventPublish.MassTransit
{
    internal sealed class MassTransitEventPublisher : IEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MassTransitEventPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task PublishAsync<TEvent>(TEvent integrationEvent, CancellationToken cancellationToken = default)
            where TEvent : class
        {
            return _publishEndpoint.Publish(integrationEvent, cancellationToken);
        }
    }
}
