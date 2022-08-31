using BunnyOwO;
using MediatR;
using Teams.Service.Abstractions;
using Teams.Service.Messages;

namespace Teams.Service.Command.CreateTodo;

public class CreateTodoCommandHandler : ICommandHandler<CreateTodoCommand, Unit>
{
    private readonly IMessageSender _messageSender;

    public CreateTodoCommandHandler(IMessageSender messageSender)
    {
        _messageSender = messageSender;
    }

    public async Task<Unit> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        var createDto = command.CreateDto;
        createDto.TeamGuid = command.TeamGuid;

        var message = new CreateTodoMessage(createDto);
        _messageSender.Publish(message, "todo.create-todo", "team.create-todo.exchange");
        
        return Unit.Value;
    }
}