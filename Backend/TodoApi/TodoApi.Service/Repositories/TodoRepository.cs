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
    private readonly TodoDbContext _todoDbContext;

    public TodoRepository(TodoDbContext todoDbContext)
    {
        _todoDbContext = todoDbContext;
    }

    public async Task<ICollection<Todo>> GetAllAsync()
    {
        var entities = await _todoDbContext.Todos.ToListAsync();
        return entities;
    }
}