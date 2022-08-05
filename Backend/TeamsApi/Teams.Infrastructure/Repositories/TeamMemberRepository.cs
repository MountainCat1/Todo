using Microsoft.EntityFrameworkCore;
using Teams.Domain.Entities;
using Teams.Domain.Repositories;
using Teams.Infrastructure.Data;

namespace Teams.Infrastructure.Repositories;

public class TeamMemberRepository : ITeamMemberRepository
{
    private readonly TeamsDbContext _dbContext;
    public TeamMemberRepository(TeamsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TeamMember?> GetAsync(Guid guid)
    {
        throw new NotImplementedException();
    }

    public async Task<TeamMember> GetRequiredAsync(Guid guid)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<TeamMember>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid guid)
    {
        throw new NotImplementedException();
    }

    public async Task<TeamMember> CreateAsync(TeamMember entity)
    {
        throw new NotImplementedException();
    }

    public async Task<TeamMember> UpdateAsync(Guid guid, TeamMember entity)
    {
        throw new NotImplementedException();
    }
}