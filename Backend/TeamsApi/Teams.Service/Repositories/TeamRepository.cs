using Teams.Domain.Entities;
using Teams.Domain.Repositories;
using Teams.Infrastructure.Data;

namespace Teams.Service.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly TeamsDbContext _dbContext;

    public TeamRepository(TeamsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<Team>> GetAllAsync(Guid? userGuid)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<Team>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Team?> GetAsync(Guid guid)
    {
        throw new NotImplementedException();
    }

    public async Task<Team> CreateAsync(Team todo)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid guid)
    {
        throw new NotImplementedException();
    }

    public async Task<Team> UpdateAsync(Guid guid, Team todo)
    {
        throw new NotImplementedException();
    }
}