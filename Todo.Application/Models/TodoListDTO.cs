namespace Todo.Application.Models;

public class TodoListDTO
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required List<TodoItemDTO> Items { get; set; }
    public int ItemsCount => Items.Count;
}
