using Todo.Domain.Entities;

namespace Todo.Domain.Interfaces;

public interface ITodoListRepository
{
    Task<IEnumerable<TodoList>> GetAll();
    Task<TodoList?> GetById(int id);
    Task Insert(TodoList todoList);
    Task Update(TodoList todoList);
    Task Delete(TodoList todoList);
}
