using Todo.Application.Common;
using Todo.Application.Exceptions;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces;

namespace Todo.Application.TodoItemFeatures;

public class DeleteTodoItemHandler : ICommandHandler<DeleteTodoItemCommand, DeleteTodoItemResponse>
{
    private readonly ITodoItemRepository _todoItemRepository;

    public DeleteTodoItemHandler(ITodoItemRepository todoItemRepository)
    {
        _todoItemRepository = todoItemRepository;
    }

    public async Task<DeleteTodoItemResponse> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var todoItem = await _todoItemRepository.GetById(request.Id) ?? throw new NotFoundException<TodoItem>();

        await _todoItemRepository.Delete(todoItem);
        return new DeleteTodoItemResponse();
    }
}

public sealed record DeleteTodoItemCommand(int Id) : ICommand<DeleteTodoItemResponse>;

public sealed record DeleteTodoItemResponse();