using System.Linq.Expressions;

namespace Authentication.Domain.Abstractions;


public interface IRepository
{
    
}
public interface IRepository<TEntity> : IRepository where TEntity : class, IEntity
{
    public Task<TEntity?> GetOneAsync(params object[] keys);
    public Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params string[] includeProperties);
    public Task<TEntity> GetOneRequiredAsync(params object[] keys);
    public Task<ICollection<TEntity>> GetAllAsync();
    public Task DeleteAsync(params object[] keys);
    public Task<TEntity> CreateAsync(TEntity entity);
    public Task<TEntity> UpdateAsync(object update, params object[] keys);
    public Task SaveChangesAsync();
}