using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Queries.GetAllAccountTeams;

public class GetAllAccountTeamsQuery : IQuery<ICollection<TeamDto>>
{
    public GetAllAccountTeamsQuery(Guid accountGuid)
    {
        AccountGuid = accountGuid;
    }

    public Guid AccountGuid { get; set; }
}