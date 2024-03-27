using Todo.Domain.Entities;

namespace Todo.Domain.Interfaces;

public interface ITodoItemRepository
{
    Task<IEnumerable<TodoItem>> GetByTodoListIds(IEnumerable<int> todoListIds);
    Task<TodoItem?> GetById(int id);
    Task Insert(TodoItem todoItem);
    Task Update(TodoItem todoItem);
    Task Delete(TodoItem todoItem);
}
