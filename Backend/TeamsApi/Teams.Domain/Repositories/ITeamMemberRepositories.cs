using Teams.Domain.Entities;

namespace Teams.Domain.Repositories;

public interface ITeamMemberRepositories
{
    public Task<ICollection<Guid>> GetMembersAsync(Guid teamGuid);
    public Task<Team> AddMember(Guid userGuid, Guid teamGuid);
    public Task DeleteMember(Guid userGuid, Guid teamGuid);
    public Task UpdateMember(TeamMember teamMember, Guid teamGuid);
}