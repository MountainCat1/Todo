namespace Teams.Domain.Abstractions;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    public Task<TEntity?> GetAsync(Guid guid);
    public Task<TEntity> GetRequiredAsync(Guid guid);
    public Task<ICollection<TEntity>> GetAllAsync();
    public Task DeleteAsync(Guid guid);
    public Task<TEntity> CreateAsync(TEntity entity);
    public Task<TEntity> UpdateAsync(Guid guid, TEntity entity);
}