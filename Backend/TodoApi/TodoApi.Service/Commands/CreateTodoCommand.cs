using TodoApi.Service.Dto;

namespace TodoApi.Service.Commands;

public class CreateTodoCommand : ICommand
{
    public CreateTodoDto CreateTodoDto { get; init; }
}