using MediatR;

namespace Todo.Application.Common;

public interface ICommand<out TResponse> : IRequest<TResponse>
{

}
