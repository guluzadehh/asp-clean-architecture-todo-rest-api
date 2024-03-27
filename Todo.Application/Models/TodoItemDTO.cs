namespace Todo.Application.Models;

public class TodoItemDTO
{
    public required int Id { get; set; }
    public required string Text { get; set; }
    public required bool IsCompleted { get; set; }
}
