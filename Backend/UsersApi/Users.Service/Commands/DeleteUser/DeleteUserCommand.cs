using Users.Service.Abstractions;

namespace Users.Service.Commands.DeleteUser;

public class DeleteUserCommand : ICommand
{
    public DeleteUserCommand(Guid guid)
    {
        Guid = guid;
    }

    public Guid Guid { get; set; }
}