using Users.Service.Abstractions;
using Users.Service.Dto;

namespace Users.Service.Commands.CreateUser;

public class CreateUserCommand : ICommand<UserDto>
{
    public CreateUserCommand(UserCreateDto createDto)
    {
        CreateDto = createDto;
    }

    public UserCreateDto CreateDto { get; set; }
}