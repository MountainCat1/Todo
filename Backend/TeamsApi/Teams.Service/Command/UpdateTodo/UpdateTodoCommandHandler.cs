using BunnyOwO;
using MediatR;
using Teams.Service.Abstractions;
using Teams.Service.Messages;

namespace Teams.Service.Command.UpdateTodo;

public class UpdateTodoCommandHandler : ICommandHandler<UpdateTodoCommand>
{
    private readonly IMessageSender _messageSender;

    public UpdateTodoCommandHandler(IMessageSender messageSender)
    {
        _messageSender = messageSender;
    }

    public async Task<Unit> Handle(UpdateTodoCommand command, CancellationToken cancellationToken)
    {
        var message = new UpdateTodoMessage(command.TodoGuid, command.UpdateTodoDto);

        _messageSender.Publish(message, "team.update-todo", "team.update-todo.exchange");
        
        return Unit.Value;
    }
}