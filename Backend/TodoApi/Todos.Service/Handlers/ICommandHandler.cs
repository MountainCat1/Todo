using Todos.Service.Commands;

namespace Todos.Service.Handlers;

public interface ICommandHandler<in T> where T : ICommand
{
    public ValueTask Handle(T command, CancellationToken ct);
}