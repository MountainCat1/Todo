using MediatR;

namespace TeamMemberships.Service.Abstractions;

public interface IQuery<out TResult> : IRequest<TResult>
{
    
}