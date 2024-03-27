namespace Todo.WebApi.Contracts;

public sealed record CreateTodoItemForm(string Text, int TodoListId);