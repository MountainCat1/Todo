using Authentication.Domain.Abstractions;
using Todos.Domain.Entities;

namespace Todos.Domain.Repositories;

public interface ITodoRepository : IRepository<Todo>
{
}