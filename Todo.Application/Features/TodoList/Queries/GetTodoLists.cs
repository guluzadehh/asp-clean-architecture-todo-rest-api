using Todo.Domain.Interfaces;
using Todo.Application.Common;
using Todo.Application.Models;
using Todo.Application.Mappers;
using Todo.Application.Services;

namespace Todo.Application.TodoListFeatures;

public class GetTodoListHandler : IQueryHandler<GetTodoListsQuery, GetTodoListsResponse>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly ITodoListService _todoListService;
    private readonly TodoListMapper _mapper;

    public GetTodoListHandler(
        ITodoListRepository todoListRepository,
        ITodoListService todoListService,
        TodoListMapper mapper)
    {
        _todoListRepository = todoListRepository;
        _todoListService = todoListService;
        _mapper = mapper;
    }

    public async Task<GetTodoListsResponse> Handle(GetTodoListsQuery request, CancellationToken cancellationToken)
    {
        var todoLists = await _todoListRepository.GetAll();
        await _todoListService.JoinItems(todoLists);

        var todoListsDTO = todoLists.Select(_mapper.MapFrom).ToList();

        return new GetTodoListsResponse(todoListsDTO, todoListsDTO.Count);
    }
}

public sealed record GetTodoListsQuery : IQuery<GetTodoListsResponse>;

public sealed record GetTodoListsResponse(List<TodoListDTO> TodoLists, int Count);

