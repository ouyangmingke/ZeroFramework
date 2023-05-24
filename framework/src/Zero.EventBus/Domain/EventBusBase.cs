using Microsoft.Extensions.DependencyInjection;

using System;
using System.Threading.Tasks;

namespace Zero.EventBus.Domain;

public abstract class EventBusBase : IEventBus
{
    protected IServiceScopeFactory ServiceScopeFactory { get; }

    protected EventBusBase(
        IServiceScopeFactory serviceScopeFactory)
    {
        ServiceScopeFactory = serviceScopeFactory;
    }

    /// <inheritdoc/>
    public abstract Task PublishAsync<TEvent>(TEvent eventData)
        where TEvent : class;

    protected abstract Task PublishToEventBusAsync(Type eventType, object eventData);
}
