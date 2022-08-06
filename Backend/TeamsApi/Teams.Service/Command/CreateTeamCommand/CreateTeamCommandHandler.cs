using AutoMapper;
using Teams.Domain.Entities;
using Teams.Domain.Repositories;
using Teams.Infrastructure.Abstractions;
using Teams.Infrastructure.UnitsOfWork;
using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Command.CreateTeamCommand;

public class CreateTeamCommandHandler : ICommandHandler<CreateTeamCommand, TeamDto>
{
    private readonly ITeamsUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTeamCommandHandler(IMapper mapper, ITeamsUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<TeamDto> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<ITeamRepository>();
        
        var entity = _mapper.Map<Team>(request.Dto);

        var createdEntity = await repository.CreateAsync(entity);
        await _unitOfWork.SaveAsync();

        var createdEntityDto = _mapper.Map<TeamDto>(createdEntity);
        
        return createdEntityDto;
    }
}