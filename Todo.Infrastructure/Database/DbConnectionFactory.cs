using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Todo.Infrastructure.Database;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _config;

    public DbConnectionFactory(IConfiguration configuration)
    {
        _config = configuration;
    }

    public IDbConnection Connect(string connectionName)
    {
        return new SqlConnection(_config.GetConnectionString(connectionName));
    }
}
