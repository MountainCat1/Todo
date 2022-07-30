using MediatR;

namespace Todos.Service.Commands.AddTodoTag;

public class AddTodoTagCommand : IRequest
{
    public Guid TodoGuid { get; set; }
    public string Tag { get; set; }
}