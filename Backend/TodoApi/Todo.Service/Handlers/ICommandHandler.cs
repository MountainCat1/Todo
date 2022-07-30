using Todo.Service.Commands;

namespace Todo.Service.Handlers;

public interface ICommandHandler<in T> where T : ICommand
{
    public ValueTask Handle(T command, CancellationToken ct);
}