using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.DependencyInjection;
using Teams.Domain.Abstractions;
using Teams.Infrastructure.Abstractions;
using Teams.Infrastructure.Data;

namespace Teams.Infrastructure.Generics;

public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;
    private readonly IServiceProvider _serviceProvider;

    private Dictionary<Type, IRepository?>? _repositories;

    private IDbContextTransaction? _transaction;
    
    public UnitOfWork(TDbContext dbContext, IServiceProvider serviceProvider)
    {
        _dbContext = dbContext;
        _serviceProvider = serviceProvider;
    }

    public IRepository<T> GetRepository<T>() where T : class, IEntity
    {
        if (_repositories is null)
            _repositories = new Dictionary<Type, IRepository?>();

        if (!_repositories.ContainsKey(typeof(T)))
        {
            _repositories.Add(typeof(T), _serviceProvider.GetService<IRepository<T>>());
        }

        var repository = _repositories[typeof(T)];
        if (repository == null)
            throw new Exception($"Repository for {typeof(T)} not found!");

        return (IRepository<T>)repository;
    }

    public async Task CommitAsync()
    {
        if (_transaction is null)
            throw new NullReferenceException("No active transaction");
        
        await _transaction.CommitAsync();
    }

    public async Task CreateTransactionAsync()
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}