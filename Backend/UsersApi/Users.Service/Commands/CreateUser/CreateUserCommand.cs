using Users.Service.Abstractions;
using Users.Service.Dto;

namespace Users.Service.Commands.CreateUser;

public class CreateUserCommand : ICommand<UserDto>
{
    public CreateUserCommand(UserCreateDto createDto)
    {
        CreateDto = createDto;
    }
    
    public CreateUserCommand(Guid accountGuid, string username, Guid userGuid)
    {
        CreateDto = new UserCreateDto()
        {
            Guid = userGuid,
            AccountGuid = accountGuid,
            Username = username
        };
    }

    public UserCreateDto CreateDto { get; set; }
}