using System.Data;
using Todo.Infrastructure.Database;

namespace Todo.Infrastructure.Repositories;

public abstract class BaseRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public BaseRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    protected IDbConnection GetConnection(string connectionName = "Default")
    {
        return _dbConnectionFactory.Connect(connectionName);
    }
}
