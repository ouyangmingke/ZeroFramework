using System;

using Zero.EventBus.Abstractions.Domain;

namespace Zero.EventBus.Domain;

public interface IEventHandlerDisposeWrapper : IDisposable
{
    IEventHandler EventHandler { get; }
}
