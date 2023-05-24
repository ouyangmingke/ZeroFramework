using System.Threading.Tasks;

namespace Zero.EventBus.Domain;

public interface IEventBus
{
    /// <summary>
    /// Triggers an event.
    /// </summary>
    /// <typeparam name="TEvent">Event type</typeparam>
    /// <param name="eventData">Related data for the event</param>
    /// <param name="onUnitOfWorkComplete">True, to publish the event at the end of the current unit of work, if available</param>
    /// <returns>The task to handle async operation</returns>
    Task PublishAsync<TEvent>(TEvent eventData)
        where TEvent : class;
}
