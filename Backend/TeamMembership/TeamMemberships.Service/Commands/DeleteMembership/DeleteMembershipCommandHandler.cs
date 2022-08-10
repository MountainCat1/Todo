using AutoMapper;
using MediatR;
using TeamMemberships.Domain.Repositories;
using TeamMemberships.Service.Abstractions;

namespace TeamMemberships.Service.Commands.DeleteMembership;

public class DeleteMembershipCommandHandler : ICommandHandler<DeleteMembershipCommand, Unit>
{
    private readonly ITeamMembershipRepository _teamMembershipRepository;

    public DeleteMembershipCommandHandler(ITeamMembershipRepository teamMembershipRepository, IMapper mapper)
    {
        _teamMembershipRepository = teamMembershipRepository;
    }

    public async Task<Unit> Handle(DeleteMembershipCommand command, CancellationToken cancellationToken)
    {
        await _teamMembershipRepository.DeleteAsync(command.TeamGuid, command.UserGuid);
        
        return Unit.Value;
    }
}