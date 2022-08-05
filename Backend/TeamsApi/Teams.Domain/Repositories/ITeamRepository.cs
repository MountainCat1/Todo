using Teams.Domain.Entities;

namespace Teams.Domain.Repositories;

public interface ITeamRepository
{
    public Task<ICollection<Team>> GetAllAsync();
    public Task<Team?> GetAsync(Guid guid);
    public Task<Team> GetRequiredAsync(Guid guid);
    public Task<Team> CreateAsync(Team team);
    public Task DeleteAsync(Guid guid);
    public Task<Team> UpdateAsync(Guid guid, Team team);
}