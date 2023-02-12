
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace Agricaltech.DL;
public class DbContext
{
    private readonly AzmoonetOptions _options;
    public DbContext(ILogger<DbContext> logger, IOptions<AzmoonetOptions> options)
    {
        _options = options.Value;
    }
    public IDbConnection CreateConnection() => new SqlConnection(_options.ConnectionString);
//  Server=(localdb)\\mssqllocaldb;Database=Test;Trusted_Connection=True;MultipleActiveResultSets=true"))
}
