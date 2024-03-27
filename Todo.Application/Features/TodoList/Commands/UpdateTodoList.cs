using FluentValidation;
using Todo.Application.Common;
using Todo.Application.Exceptions;
using Todo.Application.Mappers;
using Todo.Application.Models;
using Todo.Application.Services;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces;

namespace Todo.Application.TodoListFeatures;

public class UpdateTodoListHandler : ICommandHandler<UpdateTodoListCommand, UpdateTodoListResponse>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly ITodoListService _todoListService;
    private readonly TodoListMapper _mapper;

    public UpdateTodoListHandler(
        ITodoListRepository todoListRepository,
        ITodoListService todoListService,
        TodoListMapper mapper)
    {
        _todoListRepository = todoListRepository;
        _todoListService = todoListService;
        _mapper = mapper;
    }

    public async Task<UpdateTodoListResponse> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
    {
        var todoList = await _todoListRepository.GetById(request.Id) ?? throw new NotFoundException<TodoList>();

        todoList.Name = request.Name;
        await _todoListRepository.Update(todoList);

        await _todoListService.JoinItems(todoList);

        return new UpdateTodoListResponse(_mapper.MapFrom(todoList));
    }
}

public class UpdateTodoListValidator : AbstractValidator<UpdateTodoListCommand>
{
    public UpdateTodoListValidator()
    {
        RuleLevelCascadeMode = CascadeMode.StopOnFirstFailure;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("List name can't be empty.");
    }
}

public sealed record UpdateTodoListCommand(int Id, string Name) : ICommand<UpdateTodoListResponse>;

public sealed record UpdateTodoListResponse(TodoListDTO TodoList);
