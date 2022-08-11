using Users.Service.Abstractions;
using Users.Service.Dto;

namespace Users.Service.Commands.CreateUser;

public class CreateUserCommand : ICommand<UserDto>
{
    public UserCreateDto CreateDto { get; set; }
}