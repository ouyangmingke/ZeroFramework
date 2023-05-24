using System;

namespace Zero.EventBus.Abstractions.Domain;

public interface IEventNameProvider
{
    string GetName(Type eventType);
}
