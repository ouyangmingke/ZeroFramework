using System.Threading.Tasks;

using Zero.EventBus.Abstractions.Domain;

namespace Zero.EventBus.Abstractions.Domain.Distributed;

public interface IDistributedEventHandler<in TEvent> : IEventHandler
{
    /// <summary>
    /// Handler handles the event by implementing this method.
    /// </summary>
    /// <param name="eventData">Event data</param>
    Task HandleEventAsync(TEvent eventData);
}
