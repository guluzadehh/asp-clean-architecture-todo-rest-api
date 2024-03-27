using Todo.Domain.Interfaces;
using Todo.Domain.Entities;
using System.Data;
using Dapper;
using Todo.Infrastructure.Database;

namespace Todo.Infrastructure.Repositories;

public class TodoListRepository : BaseRepository, ITodoListRepository
{
    public TodoListRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
    {
    }

    public async Task Insert(TodoList todoList)
    {
        const string query =
            @"INSERT INTO dbo.TodoLists(Name, CreatedAt)
            OUTPUT Inserted.Id
            VALUES (@Name, @CreatedAt);";

        using IDbConnection connection = GetConnection();

        int insertedId = await connection.ExecuteScalarAsync<int>(
            query,
            new { todoList.Name, todoList.CreatedAt }
        );

        todoList.Id = insertedId;
    }

    public async Task Delete(TodoList todoList)
    {
        const string query = "DELETE FROM dbo.TodoLists WHERE dbo.TodoLists.Id = @Id";

        using IDbConnection connection = GetConnection();
        await connection.ExecuteAsync(query, new { todoList.Id });
    }

    public async Task<IEnumerable<TodoList>> GetAll()
    {
        const string query = "SELECT * FROM dbo.TodoLists;";

        using IDbConnection connection = GetConnection();
        return await connection.QueryAsync<TodoList>(query);
    }

    public async Task<TodoList?> GetById(int id)
    {
        const string query =
            @"SELECT * FROM dbo.TodoLists
            WHERE dbo.TodoLists.Id = @Id";

        using IDbConnection connection = GetConnection();
        return await connection.QuerySingleOrDefaultAsync<TodoList>(query, new { Id = id });
    }

    public async Task Update(TodoList todoList)
    {
        const string query =
            @"UPDATE dbo.TodoLists 
            SET Name = @Name
            WHERE Id = @Id;";

        using IDbConnection connection = GetConnection();
        await connection.ExecuteAsync(query, new
        {
            todoList.Id,
            todoList.Name
        });
    }
}
