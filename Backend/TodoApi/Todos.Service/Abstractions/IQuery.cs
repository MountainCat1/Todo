using MediatR;

namespace Todos.Service.Abstractions;

public interface IQuery<out TResult> : IRequest<TResult>
{
    
}