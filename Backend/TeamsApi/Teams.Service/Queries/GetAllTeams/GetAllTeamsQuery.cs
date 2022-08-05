using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Queries.GetAllTeams;

public class GetAllTeamsQuery : IQuery<ICollection<TeamDto>>
{
    // Intentionally empty
}