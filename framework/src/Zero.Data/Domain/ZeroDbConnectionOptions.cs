using System.Collections.Generic;

namespace Zero.Data.Domain;

public class ZeroDbConnectionOptions
{
    public ConnectionStrings ConnectionStrings { get; set; }

    public ZeroDbConnectionOptions()
    {
        ConnectionStrings = new ConnectionStrings();
    }

    public string GetConnectionStringOrNull(
        string connectionStringName,
        bool fallbackToDatabaseMappings = true,
        bool fallbackToDefault = true)
    {
        var connectionString = ConnectionStrings.GetOrDefault(connectionStringName);
        if (!string.IsNullOrEmpty(connectionString))
        {
            return connectionString;
        }

        if (fallbackToDefault)
        {
            connectionString = ConnectionStrings.Default;
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                return connectionString;
            }
        }

        return null;
    }
}
