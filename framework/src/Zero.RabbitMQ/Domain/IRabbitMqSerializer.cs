using System;

namespace Zero.RabbitMQ.Domain;

public interface IRabbitMqSerializer
{
    byte[] Serialize(object obj);

    object Deserialize(byte[] value, Type type);

    T Deserialize<T>(byte[] value);
}
