using Todo.Domain.Entities;

namespace Todo.Application.Services;

public interface ITodoListService
{
    Task JoinItems(TodoList todoList);
    Task JoinItems(IEnumerable<TodoList> todoLists);
}
