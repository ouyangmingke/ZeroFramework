using System;

namespace Zero.Json.Domain
{
    public interface IJsonSerializer
    {
        string Serialize(object obj);

        T Deserialize<T>(string jsonString);
        object Deserialize(Type type, string jsonString);
    }
}
