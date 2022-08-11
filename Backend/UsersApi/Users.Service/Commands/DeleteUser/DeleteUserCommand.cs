using Users.Service.Abstractions;

namespace Users.Service.Commands.DeleteUser;

public class DeleteUserCommand : ICommand
{
    public Guid Guid { get; set; }
}