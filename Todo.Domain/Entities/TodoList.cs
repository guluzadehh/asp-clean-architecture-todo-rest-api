namespace Todo.Domain.Entities;

public class TodoList : BaseEntity
{

    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<TodoItem> Items { get; set; } = [];

    public TodoList(string name)
    {
        Name = name;
        CreatedAt = DateTime.Now;
    }

    public TodoList()
    {
    }

    public TodoItem AddItem(string text)
    {
        var todoItem = new TodoItem(text, Id);
        Items.Add(todoItem);
        return todoItem;
    }
}
