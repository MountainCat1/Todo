using BunnyOwO;
using MediatR;
using Teams.Infrastructure.Dto;
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
            CreateTodo = createDto
        };
        
        // TODO that's for sure not correct and should ne called a heresy, change it later
        _sender.PublishEvent(message, "todo.event.todoCreated", "team.todo-create.exchange");
        
        return Unit.Value;
    }
}