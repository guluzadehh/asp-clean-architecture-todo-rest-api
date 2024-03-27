using Todo.Domain.Entities;
using Todo.Domain.Interfaces;
using Todo.Application.Common;
using Todo.Application.Mappers;
using Todo.Application.Exceptions;
using Todo.Application.Models;
using Todo.Application.Services;

namespace TodoApp.Application;

public class GetTodoListByIdHandler
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly ITodoListService _todoListService;
    private readonly TodoListMapper _mapper;

    public GetTodoListByIdHandler(
            ITodoListRepository todoListRepository,
            ITodoListService todoListService,
            TodoListMapper todoListMapper)
    {
        _todoListRepository = todoListRepository;
        _todoListService = todoListService;
        _mapper = todoListMapper;
    }

    public async Task<GetTodoListByIdResponse> Handle(GetTodoListByIdQuery request, CancellationToken cancellationToken)
    {
        var todoList = await _todoListRepository.GetById(request.Id) ?? throw new NotFoundException<TodoList>();

        await _todoListService.JoinItems(todoList);

        return new GetTodoListByIdResponse(_mapper.MapFrom(todoList));
    }
}

public sealed record GetTodoListByIdQuery(int Id) : IQuery<GetTodoListByIdResponse>;

public sealed record GetTodoListByIdResponse(TodoListDTO TodoList);