using System;
using System.Text.Json;

using Zero.Core.DependencyInjection;
using Zero.Json.Domain;

namespace Zero.Json.SystemTextJson
{
    public class ZeroSystemTextJsonSerializer : IJsonSerializer, ITransientDependency
    {
        public string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        public T Deserialize<T>(string jsonString)
        {
            return JsonSerializer.Deserialize<T>(jsonString);
        }

        public object Deserialize(Type type, string jsonString)
        {
            return JsonSerializer.Deserialize(jsonString, type);

        }
    }
}
