using MediatR;

namespace Authentication.Service.Abstractions;

public interface IQuery<out TResult> : IRequest<TResult>
{
    
}