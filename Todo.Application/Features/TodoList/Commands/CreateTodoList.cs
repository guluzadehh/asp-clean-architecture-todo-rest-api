using FluentValidation;
using Todo.Application.Common;
using Todo.Application.Mappers;
using Todo.Application.Models;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces;

namespace Todo.Application.TodoListFeatures;

public class CreateTodoListHandler : ICommandHandler<CreateTodoListCommand, CreateTodoListResponse>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly TodoListMapper _mapper;

    public CreateTodoListHandler(
        ITodoListRepository todoListRepository,
        TodoListMapper mapper)
    {
        _todoListRepository = todoListRepository;
        _mapper = mapper;
    }

    public async Task<CreateTodoListResponse> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var todoList = new TodoList(request.Name);
        await _todoListRepository.Insert(todoList);

        return new CreateTodoListResponse(_mapper.MapFrom(todoList));
    }
}

public sealed class CreateTodoListValidator : AbstractValidator<CreateTodoListCommand>
{
    public CreateTodoListValidator()
    {
        RuleLevelCascadeMode = CascadeMode.StopOnFirstFailure;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("List name can't be empty.");
    }
}

public sealed record CreateTodoListCommand(string Name) : ICommand<CreateTodoListResponse>;

public record CreateTodoListResponse(TodoListDTO TodoList);