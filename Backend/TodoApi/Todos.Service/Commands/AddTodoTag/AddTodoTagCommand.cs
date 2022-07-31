
using Todos.Service.Abstractions;
using Todos.Service.Dto;

namespace Todos.Service.Commands.AddTodoTag;

public class AddTodoTagCommand : ICommand
{
    public Guid TodoGuid { get; set; }
    public string Tag { get; set; }
}