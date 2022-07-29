using TodoApi.Service.Commands;

namespace TodoApi.Service.Handlers;

public interface ICommandHandler<T> where T : ICommand
{
    public ValueTask HandleAsync(T command);
}