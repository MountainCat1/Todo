using BunnyOwO;
using MediatR;
using Teams.Service.Abstractions;
using Teams.Service.Messages;

namespace Teams.Service.Command.CreateTodo;

public class CreateTodoCommandHandler : ICommandHandler<CreateTodoCommand, Unit>
{
    private readonly IMessageSender _sender;

    public CreateTodoCommandHandler(IMessageSender sender)
    {
        _sender = sender;
    }

    public async Task<Unit> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        var createDto = command.CreateDto;
        createDto.TeamGuid = command.TeamGuid;

        var message = new CreateTodoMessage()
        {
            CreateDto = createDto
        };
        
        _sender.Publish(message, "todo.message.todoCreated", "team.create-todo.exchange");
        
        return Unit.Value;
    }
}