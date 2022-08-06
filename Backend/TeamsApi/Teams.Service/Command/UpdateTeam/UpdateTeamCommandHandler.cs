using AutoMapper;
using Teams.Domain.Entities;
using Teams.Domain.Repositories;
using Teams.Infrastructure.UnitsOfWork;
using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Command.UpdateTeam;

public class UpdateTeamCommandHandler : ICommandHandler<UpdateTeamCommand, TeamDto>
{
    private readonly IMapper _mapper;
    private readonly ITeamsUnitOfWork _unitOfWork;


    public UpdateTeamCommandHandler(IMapper mapper, ITeamsUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<TeamDto> Handle(UpdateTeamCommand command, CancellationToken cancellationToken)
    {
        var teamRepository = _unitOfWork.GetRepository<ITeamRepository>();
        
        var update = _mapper.Map<Team>(command.Dto);

        var entityToUpdate = await teamRepository.GetRequiredAsync(command.Guid);

        await teamRepository.UpdateAsync(update, command.Guid);
        
        await _unitOfWork.SaveAsync();
        
        return _mapper.Map<TeamDto>(entityToUpdate);
    }
}