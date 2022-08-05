using Teams.Domain.Entities;

namespace Teams.Domain.Repositories;

public interface ITeamRepository
{
    public Task<ICollection<Team>> GetAllAsync(Guid? teamGuid, Guid? userGuid);
    public Task<ICollection<Team>> GetAllAsync();
    public Task<Team?> GetAsync(Guid guid);
    public Task<Team> CreateAsync(Team todo);
    public Task DeleteAsync(Guid guid);
    public Task<Team> UpdateAsync(Guid guid, Team todo);
}