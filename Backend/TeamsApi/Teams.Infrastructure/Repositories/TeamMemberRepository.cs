using Teams.Domain.Entities;
using Teams.Domain.Repositories;

namespace Teams.Infrastructure.Repositories;

public class TeamMemberRepository : ITeamMemberRepository
{
    public async Task<ICollection<Guid>> GetMembersAsync(Guid teamGuid)
    {
        throw new NotImplementedException();
    }

    public async Task<Team> AddMember(Guid userGuid, Guid teamGuid)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteMember(Guid userGuid, Guid teamGuid)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateMember(TeamMember teamMember, Guid teamGuid)
    {
        throw new NotImplementedException();
    }
}