using AutoMapper;
using TeamMemberships.Domain.Repositories;
using TeamMemberships.Service.Abstractions;
using TeamMemberships.Service.Dto;

namespace TeamMemberships.Service.Commands.UpdateMembership;

public class UpdateMembershipCommandHandler : ICommandHandler<UpdateMembershipCommand, TeamMembershipDto>
{
    private readonly IMapper _mapper;
    private readonly ITeamMembershipRepository _teamMembershipRepository;

    public UpdateMembershipCommandHandler(IMapper mapper, ITeamMembershipRepository teamMembershipRepository)
    {
        _mapper = mapper;
        _teamMembershipRepository = teamMembershipRepository;
    }

    public async Task<TeamMembershipDto> Handle(UpdateMembershipCommand command, CancellationToken cancellationToken)
    {
        var updatedEntity = await _teamMembershipRepository.UpdateAsync(
            command.UpdateDto, 
            command.TeamGuid, command.UserGuid);

        await _teamMembershipRepository.SaveChangesAsync();
        
        var updatedEntityDto = _mapper.Map<TeamMembershipDto>(updatedEntity);

        return updatedEntityDto;
    }
}