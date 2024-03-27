namespace Todo.Domain.Entities;

public class TodoItem : BaseEntity
{
    public string Text { get; set; }
    public bool IsCompleted { get; set; }
    public int TodoListId { get; set; }

    public TodoItem(string text, int todoListId)
    {
        Text = text;
        TodoListId = todoListId;
        IsCompleted = false;
    }

    public TodoItem()
    {
    }

    public void Complete()
    {
        IsCompleted = true;
    }

    public void Reset()
    {
        IsCompleted = false;
    }
}
