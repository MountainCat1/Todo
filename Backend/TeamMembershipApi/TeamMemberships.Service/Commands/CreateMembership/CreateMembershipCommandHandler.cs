using AutoMapper;
using MediatR;
using TeamMemberships.Domain.Entities;
using TeamMemberships.Domain.Repositories;
using TeamMemberships.Service.Abstractions;
using TeamMemberships.Service.Dto;

namespace TeamMemberships.Service.Commands.CreateMembership;

public class CreateMembershipCommandHandler : ICommandHandler<CreateMembershipCommand, TeamMembershipDto>
{
    private readonly ITeamMembershipRepository _teamMembershipRepository;
    private readonly IMapper _mapper;
    
    public CreateMembershipCommandHandler(ITeamMembershipRepository teamMembershipRepository, IMapper mapper)
    {
        _teamMembershipRepository = teamMembershipRepository;
        _mapper = mapper;
    }

    public async Task<TeamMembershipDto> Handle(CreateMembershipCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TeamMembership>(command.MembershipCreateDto);

        var createdEntity = await _teamMembershipRepository.CreateAsync(entity);

        await _teamMembershipRepository.SaveChangesAsync();
        
        var createdEntityDto = _mapper.Map<TeamMembershipDto>(createdEntity);
        
        return createdEntityDto;
    }
}