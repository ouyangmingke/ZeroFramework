using System;
using System.Reflection;

using Zero.Data.Domain;

namespace Zero.InfluxDB.Domain
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InfluxOrganizationAttribute : Attribute
    {
        public string Organization { get; set; }
        public InfluxOrganizationAttribute(string organization)
        {
            Organization = organization;
        }

        public static string GetOrganization(Type type)
        {
            var nameAttribute = type.GetTypeInfo().GetCustomAttribute<ConnectionStringNameAttribute>();

            if (nameAttribute == null)
            {
                return type.FullName;
            }

            return nameAttribute.Name;
        }
    }
}
