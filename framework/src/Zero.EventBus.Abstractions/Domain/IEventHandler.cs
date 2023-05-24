using Zero.EventBus.Abstractions.Domain.Distributed;

namespace Zero.EventBus.Abstractions.Domain;

/// <summary>
/// Indirect base interface for all event handlers.
/// Implement <see cref="ILocalEventHandler{TEvent}"/> or <see cref="IDistributedEventHandler{TEvent}"/> instead of this one.
/// </summary>
public interface IEventHandler
{

}
