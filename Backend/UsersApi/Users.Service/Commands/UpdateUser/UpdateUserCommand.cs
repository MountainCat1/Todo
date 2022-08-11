using Users.Service.Abstractions;
using Users.Service.Dto;

namespace Users.Service.Commands.UpdateUser;

public class UpdateUserCommand : ICommand<UserDto>
{
    public Guid Guid { get; set; }
    public UserUpdateDto UpdateDto { get; set; }
}