using MediatR;
using Users.Domain.Repositories;
using Users.Service.Abstractions;

namespace Users.Service.Commands.DeleteUser;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteAsync(command.Guid);

        await _userRepository.SaveChangesAsync();
        
        return Unit.Value;
    }
}