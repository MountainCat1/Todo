using MediatR;
using Teams.Domain.Repositories;
using Teams.Service.Abstractions;

namespace Teams.Service.Command.DeleteTeam;

public class DeleteTeamCommandHandler : ICommandHandler<DeleteTeamCommand, Unit>
{
    private readonly ITeamRepository _teamRepository;

    public DeleteTeamCommandHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Unit> Handle(DeleteTeamCommand command, CancellationToken cancellationToken)
    {
        await _teamRepository.DeleteAsync(command.Guid);

        await _teamRepository.SaveChangesAsync();
        
        return Unit.Value;
    }
}