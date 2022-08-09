using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Teams.Domain.Abstractions;
using Teams.Infrastructure.Exceptions;

namespace Teams.Infrastructure.Generics;

public class Repository<TEntity, TDbContext> : IRepository<TEntity> 
    where TEntity : class, IEntity
    where TDbContext : DbContext
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly Func<Task> _saveChangesAsyncDelegate;


    public Repository(TDbContext dbContext)
    {
        _dbSet = dbContext.Set<TEntity>();

        _saveChangesAsyncDelegate = async () =>
        {
            await dbContext.SaveChangesAsync();
        };
    }

    public async Task<TEntity?> GetAsync(object[] guids)
    {
        if (guids.Length == 0)
            throw new ArgumentException("Guid was not provided!");
        
        var entity = await _dbSet.FindAsync(guids);
        
        return entity;
    }
    
    public virtual async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params string[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public async Task<TEntity> GetRequiredAsync(params object[] guids)
    {
        var entity = await GetAsync(guids);

        if (entity == null)
            throw new ItemNotFoundException();

        return entity;
    }

    public async Task<ICollection<TEntity>> GetAllAsync()
    {
        var entities = await _dbSet.ToListAsync();
        
        return entities;
    }

    public async Task DeleteAsync(object guid)
    {
        var entity = await GetRequiredAsync(guid);

        _dbSet.Remove(entity);
    }

    public Task<TEntity> CreateAsync(TEntity entity)
    {
        _dbSet.Add(entity);
        
        return Task.FromResult(entity);
    }

    public async Task<TEntity> UpdateAsync(object update, params object[] keys)
    {
        var entity = await GetRequiredAsync(keys);

        _dbSet.Attach(entity).CurrentValues.SetValues(update);
        _dbSet.Attach(entity).State = EntityState.Modified;

        return entity;
    }

    public async Task SaveChangesAsync()
    {
        await _saveChangesAsyncDelegate();
    }
}