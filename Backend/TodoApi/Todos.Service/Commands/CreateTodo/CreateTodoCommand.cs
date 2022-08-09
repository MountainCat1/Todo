using MediatR;
using Todos.Service.Abstractions;
using Todos.Service.Dto;

namespace Todos.Service.Commands.CreateTodo;

public class CreateTodoCommand : ICommand<TodoDto>
{
    public CreateTodoCommand(CreateTodoDto dto)
    {
        Dto = dto;
    }

    public CreateTodoDto Dto { get; set; }
}