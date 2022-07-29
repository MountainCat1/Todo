using Microsoft.EntityFrameworkCore;
using TodoApi.Data.Services;
using TodoApi.Domain.Entities;

namespace TodoApi.Data.Repositories;

public interface ITodoRepository
{
    public Task<ICollection<Todo>> GetAllAsync();
    public Task<Todo?> GetAsync(Guid guid);
    public Task<Todo> CreateAsync(Todo todo);
    public Task DeleteAsync(Guid guid);
    public Task<Todo> UpdateAsync(Guid guid, Todo todo);
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

    public async Task<Todo?> GetAsync(Guid guid)
    {
        var entity = await _todoDbContext.Todos.FindAsync(guid);
        return entity;
    }

    public async Task<Todo> CreateAsync(Todo todo)
    {
        todo.Guid = Guid.NewGuid();
        
        _todoDbContext.Todos.Add(todo);
        await _todoDbContext.SaveChangesAsync();
        
        return todo;
    }

    public async Task DeleteAsync(Guid guid)
    {
        var entityToDelete = await GetAsync(guid);

        if (entityToDelete is null)
            throw new ArgumentException($"Todo not found (guid: {guid})");

        _todoDbContext.Todos.Remove(entityToDelete);
        await _todoDbContext.SaveChangesAsync();
    }

    public async Task<Todo> UpdateAsync(Guid guid, Todo todo)
    {
        var entityToUpdate = await GetAsync(guid);

        if (entityToUpdate is null)
            throw new ArgumentException($"Todo not found (guid: {guid})");

        todo.Guid = entityToUpdate.Guid;

        _todoDbContext.Todos.Update(todo);
        await _todoDbContext.SaveChangesAsync();

        return todo;
    }
}