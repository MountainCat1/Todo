using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Todo.Data.Services;
using Todo.Domain.Entities;

namespace Todo.Data.Repositories;

public interface ITodoRepository
{
    public Task<ICollection<Domain.Entities.Todo>> GetAllAsync(Guid? teamGuid, Guid? userGuid);
    public Task<ICollection<Domain.Entities.Todo>> GetAllAsync();
    public Task<Domain.Entities.Todo?> GetAsync(Guid guid);
    public Task<Domain.Entities.Todo> CreateAsync(Domain.Entities.Todo todo);
    public Task DeleteAsync(Guid guid);
    public Task<Domain.Entities.Todo> UpdateAsync(Guid guid, Domain.Entities.Todo todo);
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

    public async Task<ICollection<Domain.Entities.Todo>> GetAllAsync(Guid? teamGuid, Guid? userGuid)
    {
        _logger.LogInformation($"Returning filtered entities...");
        
        IQueryable<Domain.Entities.Todo> query = _todoDbContext.Todos;

        if (teamGuid != null)
            query = query.Where(entity => entity.TeamGuid == teamGuid);

        if (userGuid != null)
            query = query.Where(entity => entity.UserGuid == userGuid);

        var entities = await query.ToListAsync();
        return entities;
    }

    public async Task<ICollection<Domain.Entities.Todo>> GetAllAsync()
    {
        _logger.LogInformation($"Returning all entities...");
        
        var entities = await _todoDbContext.Todos.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entities.Todo?> GetAsync(Guid guid)
    {
        _logger.LogInformation($"Returning entity... {guid}");
        
        var entity = await _todoDbContext.Todos.FindAsync(guid);
        return entity;
    }

    public async Task<Domain.Entities.Todo> CreateAsync(Domain.Entities.Todo todo)
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
            throw new ArgumentException($"Todo.Api not found (guid: {guid})");

        _todoDbContext.Todos.Remove(entityToDelete);
        await _todoDbContext.SaveChangesAsync();
    }

    public async Task<Domain.Entities.Todo> UpdateAsync(Guid guid, Domain.Entities.Todo todo)
    {
        _logger.LogInformation($"Updating entity... {guid}");
        var entityToUpdate = await GetAsync(guid);

        if (entityToUpdate is null)
            throw new ArgumentException($"Todo.Api not found (guid: {guid})");

        todo.Guid = entityToUpdate.Guid;

        _todoDbContext.Todos.Update(todo);
        await _todoDbContext.SaveChangesAsync();

        return todo;
    }
}