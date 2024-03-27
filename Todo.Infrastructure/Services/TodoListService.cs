using Todo.Application.Services;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces;

namespace Todo.Infrastructure.Services;

public class TodoListService : ITodoListService
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly ITodoItemRepository _todoItemRepository;

    public TodoListService(
        ITodoListRepository todoListRepository,
        ITodoItemRepository todoItemRepository)
    {
        _todoListRepository = todoListRepository;
        _todoItemRepository = todoItemRepository;
    }

    public async Task JoinItems(TodoList todoList)
    {
        var todoItems = await _todoItemRepository.GetByTodoListIds([todoList.Id]);
        todoList.Items.AddRange(todoItems);
    }

    public async Task JoinItems(IEnumerable<TodoList> todoLists)
    {
        if (!todoLists.Any())
        {
            return;
        }

        int[] todoListIds = todoLists.Select(list => list.Id).ToArray();

        IEnumerable<TodoItem> todoItems = await _todoItemRepository.GetByTodoListIds(todoListIds);
        Dictionary<int, List<TodoItem>> listItemsRelation = [];

        foreach (var item in todoItems)
        {
            if (!listItemsRelation.ContainsKey(item.TodoListId))
            {
                listItemsRelation[item.TodoListId] = [];
            }

            listItemsRelation[item.TodoListId].Add(item);
        }

        foreach (var list in todoLists)
        {
            list.Items.AddRange(listItemsRelation.GetValueOrDefault(list.Id, []));
        }
    }
}
