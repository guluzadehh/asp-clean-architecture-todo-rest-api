using MediatR;

namespace Todo.Application.Common;

public interface IQuery<out TResponse> : IRequest<TResponse>
{

}
