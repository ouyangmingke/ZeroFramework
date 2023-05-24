using System.Threading.Tasks;

namespace Zero.EventBus.Domain.Distributed;

public interface IDistributedEventBus : IEventBus
{
    Task PublishAsync<TEvent>(
        TEvent eventData)
        where TEvent : class;

}
