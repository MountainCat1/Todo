using Users.Service.Abstractions;
using Users.Service.Dto;

namespace Users.Service.Commands.UpdateUser;

public class UpdateUserCommand : ICommand<UserDto>
{
    public UpdateUserCommand(Guid guid, UserUpdateDto updateDto)
    {
        Guid = guid;
        UpdateDto = updateDto;
    }

    public Guid Guid { get; set; }
    public UserUpdateDto UpdateDto { get; set; }
}