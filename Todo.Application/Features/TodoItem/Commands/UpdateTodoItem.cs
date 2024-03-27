using Todo.Application.Common;
using Todo.Application.Exceptions;
using Todo.Application.Mappers;
using Todo.Application.Models;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces;

namespace Todo.Application;

public class UpdateTodoItemHandler : ICommandHandler<UpdateTodoItemCommand, UpdateTodoItemResponse>
{
    private readonly ITodoItemRepository _todoItemRepository;
    private readonly TodoItemMapper _mapper;

    public UpdateTodoItemHandler(
        ITodoItemRepository todoItemRepository,
        TodoItemMapper mapper)
    {
        _todoItemRepository = todoItemRepository;
        _mapper = mapper;
    }

    public async Task<UpdateTodoItemResponse> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var todoItem = await _todoItemRepository.GetById(request.Id) ?? throw new NotFoundException<TodoItem>();

        todoItem.Text = request.Text;

        await _todoItemRepository.Update(todoItem);

        return new UpdateTodoItemResponse(_mapper.MapFrom(todoItem));
    }
}

public sealed record UpdateTodoItemResponse(TodoItemDTO TodoItem);

public sealed record UpdateTodoItemCommand(int Id, string Text) : ICommand<UpdateTodoItemResponse>;