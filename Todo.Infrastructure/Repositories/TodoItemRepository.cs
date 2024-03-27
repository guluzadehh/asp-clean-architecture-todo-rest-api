using Todo.Domain.Interfaces;
using Todo.Domain.Entities;
using System.Data;
using Dapper;
using Todo.Infrastructure.Database;

namespace Todo.Infrastructure.Repositories;

public class TodoItemRepository : BaseRepository, ITodoItemRepository
{
    public TodoItemRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
    {
    }

    public async Task Insert(TodoItem todoItem)
    {
        const string query =
            @"INSERT INTO dbo.TodoItems(Text, TodoListId)
            OUTPUT Inserted.Id
            VALUES (@Text, @TodoListId)";

        using IDbConnection connection = GetConnection();
        int insertedId = await connection.ExecuteScalarAsync<int>(
            query,
            new { todoItem.Text, todoItem.TodoListId }
        );

        todoItem.Id = insertedId;
    }

    public async Task<TodoItem?> GetById(int id)
    {
        const string query =
            @"SELECT * FROM dbo.TodoItems
            WHERE Id = @Id";

        using IDbConnection connection = GetConnection();
        return await connection.QuerySingleOrDefaultAsync<TodoItem>(query, new { Id = id });
    }

    public async Task<IEnumerable<TodoItem>> GetByTodoListIds(IEnumerable<int> todoListIds)
    {
        const string query =
            @"SELECT * FROM dbo.TodoItems
            WHERE dbo.TodoItems.TodoListId IN @TodoListIds;";

        using IDbConnection connection = GetConnection();
        return await connection.QueryAsync<TodoItem>(query, new { TodoListIds = todoListIds });
    }

    public async Task Update(TodoItem todoItem)
    {
        const string query =
            @"UPDATE dbo.TodoItems
            SET Text = @Text, IsCompleted = @IsCompleted
            WHERE Id = @Id;";

        using IDbConnection connection = GetConnection();
        await connection.ExecuteAsync(query, new
        {
            todoItem.Id,
            todoItem.Text,
            todoItem.IsCompleted
        });
    }

    public async Task Delete(TodoItem todoItem)
    {
        const string query =
            @"DELETE FROM dbo.TodoItems
            WHERE dbo.TodoItems.Id = @Id";

        using IDbConnection connection = GetConnection();
        await connection.ExecuteAsync(query, new { todoItem.Id });
    }
}
