using Todo.Application.Common;
using Todo.Application.Exceptions;
using Todo.Application.Mappers;
using Todo.Application.Models;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces;

namespace Todo.Application.TodoItemFeatures;

public class CompleteTodoItemHandler : ICommandHandler<CompleteTodoItemCommand, CompleteTodoItemResponse>
{
    private readonly ITodoItemRepository _todoItemRepository;
    private readonly TodoItemMapper _mapper;

    public CompleteTodoItemHandler(
        ITodoItemRepository todoItemRepository,
        TodoItemMapper mapper)
    {
        _todoItemRepository = todoItemRepository;
        _mapper = mapper;
    }

    public async Task<CompleteTodoItemResponse> Handle(CompleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var todoItem = await _todoItemRepository.GetById(request.Id) ?? throw new NotFoundException<TodoItem>();

        todoItem.Complete();
        await _todoItemRepository.Update(todoItem);

        return new CompleteTodoItemResponse(_mapper.MapFrom(todoItem));
    }
}

public sealed record CompleteTodoItemCommand(int Id) : ICommand<CompleteTodoItemResponse>;

public sealed record CompleteTodoItemResponse(TodoItemDTO TodoItem);
