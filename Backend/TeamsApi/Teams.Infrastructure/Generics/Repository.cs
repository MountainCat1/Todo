using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Teams.Domain.Abstractions;
using Teams.Infrastructure.Exceptions;

namespace Teams.Infrastructure.Generics;

public class Repository<TEntity, TDbContext> : IRepository<TEntity> 
    where TEntity : class, IEntity
    where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;
    private readonly ILogger<Repository<TEntity, TDbContext>> _logger;

    public Repository(TDbContext dbContext, ILogger<Repository<TEntity, TDbContext>> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<TEntity?> GetAsync(Guid guid)
    {
        var entity = await _dbContext
            .FindAsync<TEntity>(guid);

        _logger.LogInformation($"Returning entity: {guid}");
        return entity;
    }

    public async Task<TEntity> GetRequiredAsync(Guid guid)
    {
        var entity = await GetAsync(guid);

        if (entity == null)
            throw new ItemNotFoundException();

        return entity;
    }

    public async Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity,bool>> predicate)
    {
        var entities = await _dbContext
            .Set<TEntity>()
            .Where(predicate)
            .ToListAsync();

        return entities;
    }

    public async Task<ICollection<TEntity>> GetAllAsync()
    {
        var entities = await _dbContext
            .Set<TEntity>()
            .ToListAsync();

        _logger.LogInformation($"Returning all entities");
        return entities;
    }

    public async Task DeleteAsync(Guid guid)
    {
        var entity = await GetRequiredAsync(guid);

        _dbContext.Remove<TEntity>(entity);

        _logger.LogInformation($"Deleting entity: {guid}");
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        entity.Guid = new Guid();

        _dbContext.Add(entity);

        _logger.LogInformation($"Creating entity: {entity.Guid}");
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        _dbContext.Add(entity);

        _logger.LogInformation($"Inserting entity: {entity.Guid}");
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(Guid guid, TEntity entity)
    {
        var entityToUpdate = await GetRequiredAsync(guid);

        entityToUpdate.Guid = guid;

        _dbContext.Update<TEntity>(entityToUpdate);

        _logger.LogInformation($"Updating entity: {guid}");
        await _dbContext.SaveChangesAsync();
        return entityToUpdate;
    }
}