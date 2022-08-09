using System.Linq.Expressions;

namespace TeamMemberships.Domain.Abstractions;


public interface IRepository
{
    
}
public interface IRepository<TEntity> : IRepository where TEntity : class, IEntity
{
    public Task<TEntity?> GetAsync(params object[] guids);
    public Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params string[] includeProperties);
    public Task<TEntity> GetRequiredAsync(params object[] guids);
    public Task<ICollection<TEntity>> GetAllAsync();
    public Task DeleteAsync(object guid);
    public Task<TEntity> CreateAsync(TEntity entity);
    public Task<TEntity> UpdateAsync(object update, params object[] keys);
    public Task SaveChangesAsync();
}