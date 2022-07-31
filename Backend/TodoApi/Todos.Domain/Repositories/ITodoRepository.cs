using Todos.Domain.Entities;

namespace Todos.Domain.Repositories;

public interface ITodoRepository
{
    public Task<ICollection<Todo>> GetAllAsync(Guid? teamGuid, Guid? userGuid);
    public Task<ICollection<Todo>> GetAllAsync();
    public Task<Todo?> GetAsync(Guid guid);
    public Task<Todo> CreateAsync(Todo todo);
    public Task DeleteAsync(Guid guid);
    public Task<Todo> UpdateAsync(Guid guid, Todo todo);
}