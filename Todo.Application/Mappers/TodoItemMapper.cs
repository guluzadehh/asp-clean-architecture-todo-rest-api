using Todo.Application.Models;
using Todo.Domain.Entities;

namespace Todo.Application.Mappers;

public class TodoItemMapper
{
    public TodoItemDTO MapFrom(TodoItem todoItem)
    {
        return new TodoItemDTO
        {
            Id = todoItem.Id,
            Text = todoItem.Text,
            IsCompleted = todoItem.IsCompleted
        };
    }
}
