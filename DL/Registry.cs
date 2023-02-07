using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

public class RegistryBLContext
{
    private string? connectionString="";

        // IConfiguration config = new ConfigurationBuilder()
        //     .AddJsonFile("appsettings.json")
        //     .AddEnvironmentVariables()
        //     .Build();
 
    public IDbConnection CreateConnection() => new SqlConnection(connectionString);
}
