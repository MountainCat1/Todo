using MediatR;

namespace Todos.Service.Abstractions;

public interface ICommand<TResult> : IRequest<TResult>
{
}

public interface ICommand : IRequest
{
}