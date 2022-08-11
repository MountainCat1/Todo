using MediatR;

namespace Users.Service.Abstractions;

public interface IQuery<out TResult> : IRequest<TResult>
{
    
}