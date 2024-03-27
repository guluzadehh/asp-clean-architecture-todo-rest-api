using System.Data;

namespace Todo.Infrastructure.Database;

public interface IDbConnectionFactory
{
    IDbConnection Connect(string connectionName);
}
