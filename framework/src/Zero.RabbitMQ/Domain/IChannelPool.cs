using System;

namespace Zero.RabbitMQ.Domain;

public interface IChannelPool : IDisposable
{
    IChannelAccessor Acquire(string channelName = null, string connectionName = null);
}
