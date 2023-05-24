using System;

using RabbitMQ.Client;

namespace Zero.RabbitMQ.Domain;

/// <summary>
/// 连接池
/// </summary>
public interface IConnectionPool : IDisposable
{
    IConnection Get(string connectionName = null);
}
