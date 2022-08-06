using Microsoft.EntityFrameworkCore;
using Teams.Domain.Abstractions;

namespace Teams.Infrastructure.Abstractions;

public interface IUnitOfWork<TDbContext>
    where TDbContext : DbContext
{
    public IRepository<T> GetRepository<T>() where T : class, IEntity;
    public Task CommitAsync();
    public Task CreateTransactionAsync();
    Task SaveAsync();
}