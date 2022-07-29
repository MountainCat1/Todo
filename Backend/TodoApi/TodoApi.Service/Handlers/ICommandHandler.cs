using TodoApi.Service.Commands;

namespace TodoApi.Service.Handlers;

public interface ICommandHandler<in T> where T : ICommand
{
    public ValueTask Handle(T command, CancellationToken ct);
}