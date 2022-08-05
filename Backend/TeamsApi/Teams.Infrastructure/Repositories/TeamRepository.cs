using Microsoft.EntityFrameworkCore;
using Teams.Domain.Entities;
using Teams.Domain.Repositories;
using Teams.Infrastructure.Data;
using Teams.Infrastructure.Exceptions;

namespace Teams.Infrastructure.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly TeamsDbContext _dbContext;

    public TeamRepository(TeamsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ICollection<Team>> GetAllAsync()
    {
        var entities = await _dbContext.Teams.ToListAsync();
        return entities;
    }

    public async Task<Team?> GetAsync(Guid guid)
    {
        var entity = await _dbContext.Teams.FindAsync(guid);
        return entity;
    }

    public async Task<Team> GetRequiredAsync(Guid guid)
    {
        var entity = await GetAsync(guid);
        
        if (entity == null)
            throw new ItemNotFoundException($"Team not found (guid: {guid})");
        
        return entity;
    }

    public async Task<Team> CreateAsync(Team team)
    {
        _dbContext.Teams.Add(team);
        
        await _dbContext.SaveChangesAsync();
        
        return team;
    }

    public async Task DeleteAsync(Guid guid)
    {
        var entityToDelete = await GetRequiredAsync(guid);
        
        _dbContext.Teams.Remove(entityToDelete);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Team> UpdateAsync(Guid guid, Team team)
    {
        var entityToUpdate = await GetRequiredAsync(guid);
        
        team.Guid = entityToUpdate.Guid;
        
        _dbContext.Teams.Update(entityToUpdate);
        await _dbContext.SaveChangesAsync();
        return team;
    }
}