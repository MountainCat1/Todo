using Microsoft.EntityFrameworkCore;
using TodoApi.Data.Services;
using TodoApi.Domain.Entities;

namespace TodoApi.Service.Repositories;

public interface ITodoRepository
{
    public Task<ICollection<Todo>> GetAllAsync();
}

public class TodoRepository : ITodoRepository
{
    private readonly DatabaseContext _databaseContext;

    public TodoRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<ICollection<Todo>> GetAllAsync()
    {
        var entities = await _databaseContext.Todos.ToListAsync();
        return entities;
    }
}