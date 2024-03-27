using FluentValidation;
using Todo.Application.Common;
using Todo.Application.Exceptions;
using Todo.Application.Mappers;
using Todo.Application.Models;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces;

namespace Todo.Application.TodoItemFeatures;

public class CreateTodoItemHandler : ICommandHandler<CreateTodoItemCommand, CreateTodoItemResponse>
{
    private readonly ITodoItemRepository _todoItemRepository;
    private readonly ITodoListRepository _todoListRepository;
    private readonly TodoItemMapper _mapper;

    public CreateTodoItemHandler(
        ITodoItemRepository todoItemRepository,
        ITodoListRepository todoListRepository,
        TodoItemMapper mapper)
    {
        _todoItemRepository = todoItemRepository;
        _todoListRepository = todoListRepository;
        _mapper = mapper;
    }

    public async Task<CreateTodoItemResponse> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var todoList = await _todoListRepository.GetById(request.TodoListId);

        if (todoList is null)
        {
            throw new NotFoundException<TodoList>(); // !!!!
        }

        var todoItem = todoList.AddItem(request.Text);
        await _todoItemRepository.Insert(todoItem);

        return new CreateTodoItemResponse(_mapper.MapFrom(todoItem));
    }
}

public class CreateTodoItemValidator : AbstractValidator<CreateTodoItemCommand>
{
    public CreateTodoItemValidator()
    {
        RuleLevelCascadeMode = CascadeMode.StopOnFirstFailure;

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Item text can't be empty.");
    }
}


public sealed record CreateTodoItemResponse(TodoItemDTO TodoItem);

public sealed record CreateTodoItemCommand(string Text, int TodoListId) : ICommand<CreateTodoItemResponse>;
