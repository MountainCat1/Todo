using MediatR;

namespace Teams.Service.Abstractions;

public interface IQuery<out TResult> : IRequest<TResult>
{
    
}