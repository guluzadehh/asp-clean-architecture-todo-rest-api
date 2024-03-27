using Todo.Application.Common;
using Todo.Application.Exceptions;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces;

namespace Todo.Application.TodoListFeatures;

public class DeleteTodoListCommandHandler : ICommandHandler<DeleteTodoListCommand, DeleteTodoListResponse>
{
    private readonly ITodoListRepository _todoListRepository;

    public DeleteTodoListCommandHandler(ITodoListRepository todoListRepository)
    {
        _todoListRepository = todoListRepository;
    }

    public async Task<DeleteTodoListResponse> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
    {
        var todoList = await _todoListRepository.GetById(request.Id) ?? throw new NotFoundException<TodoList>();
        await _todoListRepository.Delete(todoList);
        return new DeleteTodoListResponse();
    }
}

public sealed record DeleteTodoListCommand(int Id) : ICommand<DeleteTodoListResponse>;

public sealed record DeleteTodoListResponse();
