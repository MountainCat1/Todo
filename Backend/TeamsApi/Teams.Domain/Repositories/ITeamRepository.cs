using Teams.Domain.Entities;

namespace Teams.Domain.Repositories;

public interface ITeamRepository
{
    public Task<ICollection<Team>> GetAllAsync(Guid? userGuid);
    public Task<ICollection<Team>> GetAllAsync();
    public Task<Team?> GetAsync(Guid guid);
    public Task<Team> CreateAsync(Team todo);
    public Task DeleteAsync(Guid guid);
    public Task<Team> UpdateAsync(Guid guid, Team todo);

    public Task<ICollection<Guid>> GetMembersAsync(Guid teamGuid);
    public Task<Team> AddMember(Guid userGuid, Guid teamGuid);
    public Task DeleteMember(Guid userGuid, Guid teamGuid);
    public Task UpdateMember(TeamMember teamMember, Guid teamGuid);
}