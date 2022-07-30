using MediatR;
using Todos.Service.Dto;

namespace Todos.Service.Commands.CreateTodo;

public class CreateTodoCommand : IRequest
{
    public CreateTodoCommand(CreateTodoDto dto)
    {
        Dto = dto;
    }

    public CreateTodoDto Dto { get; set; }
}