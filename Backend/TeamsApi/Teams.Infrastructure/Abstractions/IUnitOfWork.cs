using Microsoft.EntityFrameworkCore;
using Teams.Domain.Abstractions;

namespace Teams.Infrastructure.Abstractions;

public interface IUnitOfWork<TDbContext>
    where TDbContext : DbContext
{
    public IRepository<T> GetEntityRepository<T>() where T : class, IEntity;
    TRepository GetRepository<TRepository>()
        where TRepository : class, IRepository;
    public Task CommitAsync();
    public Task CreateTransactionAsync();
    Task SaveAsync();


}