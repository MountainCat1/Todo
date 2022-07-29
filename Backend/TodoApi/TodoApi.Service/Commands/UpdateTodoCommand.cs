using TodoApi.Service.Dto;

namespace TodoApi.Service.Commands;

public class UpdateTodoCommand : ICommand
{
    public Guid Guid { get; init; }
    public TodoDto TodoDto { get; init; }
}