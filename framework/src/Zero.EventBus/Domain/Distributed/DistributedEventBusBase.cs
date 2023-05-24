using Microsoft.Extensions.DependencyInjection;

using System;
using System.Threading.Tasks;

namespace Zero.EventBus.Domain.Distributed
{
    public abstract class DistributedEventBusBase : EventBusBase, IDistributedEventBus
    {
        public DistributedEventBusBase(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        public override async Task PublishAsync<TEvent>(TEvent eventData)
        {
            await PublishAsync(typeof(TEvent), eventData);
        }
        public async Task PublishAsync(Type eventType, object eventData)
        {
            await PublishToEventBusAsync(eventType, eventData);
        }
    }
}
