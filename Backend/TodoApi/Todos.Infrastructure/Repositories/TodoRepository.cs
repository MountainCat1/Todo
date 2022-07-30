using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Todos.Domain.Entities;
using Todos.Infrastructure.Services;

namespace Todos.Infrastructure.Repositories;

public interface ITodoRepository
{
    public Task<ICollection<Todo>> GetAllAsync(Guid? teamGuid, Guid? userGuid);
    public Task<ICollection<Todo>> GetAllAsync();
    public Task<Todo?> GetAsync(Guid guid);
    public Task<Todo> CreateAsync(Todo todo);
    public Task DeleteAsync(Guid guid);
    public Task<Todo> UpdateAsync(Guid guid, Todo todo);
}

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _todoDbContext;
    private readonly ILogger<TodoRepository> _logger;

    public TodoRepository(TodoDbContext todoDbContext, ILogger<TodoRepository> logger)
    {
        _todoDbContext = todoDbContext;
        _logger = logger;
    }

    public async Task<ICollection<Todo>> GetAllAsync(Guid? teamGuid, Guid? userGuid)
    {
        _logger.LogInformation($"Returning filtered entities...");
        
        IQueryable<Todo> query = _todoDbContext.Todos;

        if (teamGuid != null)
            query = query.Where(entity => entity.TeamGuid == teamGuid);

        if (userGuid != null)
            query = query.Where(entity => entity.UserGuid == userGuid);

        var entities = await query.ToListAsync();
        return entities;
    }

    public async Task<ICollection<Todo>> GetAllAsync()
    {
        _logger.LogInformation($"Returning all entities...");
        
        var entities = await _todoDbContext.Todos.ToListAsync();
        return entities;
    }

    public async Task<Todo?> GetAsync(Guid guid)
    {
        _logger.LogInformation($"Returning entity... {guid}");
        
        var entity = await _todoDbContext.Todos.FindAsync(guid);
        return entity;
    }

    public async Task<Todo> CreateAsync(Todo todo)
    {
        todo.Guid = Guid.NewGuid();
        _logger.LogInformation($"Creating new entity... {todo.Guid}");
        
        _todoDbContext.Todos.Add(todo);
        await _todoDbContext.SaveChangesAsync();
        
        return todo;
    }

    public async Task DeleteAsync(Guid guid)
    {
        _logger.LogInformation($"Deleting entity... {guid}");
        var entityToDelete = await GetAsync(guid);

        if (entityToDelete is null)
            throw new ArgumentException($"Todos.Api not found (guid: {guid})");

        _todoDbContext.Todos.Remove(entityToDelete);
        await _todoDbContext.SaveChangesAsync();
    }

    public async Task<Todo> UpdateAsync(Guid guid, Todo todo)
    {
        _logger.LogInformation($"Updating entity... {guid}");
        var entityToUpdate = await GetAsync(guid);

        if (entityToUpdate is null)
            throw new ArgumentException($"Todos.Api not found (guid: {guid})");

        todo.Guid = entityToUpdate.Guid;

        _todoDbContext.Todos.Update(todo);
        await _todoDbContext.SaveChangesAsync();

        return todo;
    }
}