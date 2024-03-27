using Todo.Application.Models;
using Todo.Domain.Entities;

namespace Todo.Application.Mappers;

public class TodoListMapper
{
    private readonly TodoItemMapper _todoItemMapper;

    public TodoListMapper(TodoItemMapper todoItemMapper)
    {
        _todoItemMapper = todoItemMapper;
    }

    public TodoListDTO MapFrom(TodoList todoList)
    {
        return new TodoListDTO
        {
            Id = todoList.Id,
            Name = todoList.Name,
            CreatedAt = todoList.CreatedAt,
            Items = todoList.Items.Select(item => _todoItemMapper.MapFrom(item)).ToList()
        };
    }
}
