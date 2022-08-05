using Microsoft.EntityFrameworkCore;
using Teams.Domain.Abstractions;
using Teams.Infrastructure.Exceptions;

namespace Teams.Infrastructure.Generics;

public class Repository<TEntity, TDbContext> : IRepository<TEntity> 
    where TEntity : class, IEntity
    where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;

    public Repository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity?> GetAsync(Guid guid)
    {
        var entity = await _dbContext
            .FindAsync<TEntity>(guid);

        return entity;
    }

    public async Task<TEntity> GetRequiredAsync(Guid guid)
    {
        var entity = await GetAsync(guid);

        if (entity == null)
            throw new ItemNotFoundException();

        return entity;
    }

    public async Task<ICollection<TEntity>> GetAllAsync()
    {
        var entities = await _dbContext
            .Set<TEntity>()
            .ToListAsync();

        return entities;
    }

    public async Task DeleteAsync(Guid guid)
    {
        var entity = await GetRequiredAsync(guid);

        _dbContext.Remove<TEntity>(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        entity.Guid = new Guid();

        _dbContext.Add(entity);

        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        _dbContext.Add(entity);

        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(Guid guid, TEntity entity)
    {
        var entityToUpdate = await GetRequiredAsync(guid);

        entityToUpdate.Guid = guid;

        _dbContext.Update<TEntity>(entityToUpdate);

        await _dbContext.SaveChangesAsync();
        return entityToUpdate;
    }
}