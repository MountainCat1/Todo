using MediatR;

namespace TeamMemberships.Service.Abstractions;

public interface ICommand<TResult> : IRequest<TResult>
{
}

public interface ICommand : ICommand<Unit>
{
}