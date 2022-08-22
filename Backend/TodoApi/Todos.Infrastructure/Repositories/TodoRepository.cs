using Authentication.Infrastructure.Generics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Todos.Domain.Entities;
using Todos.Domain.Exceptions;
using Todos.Domain.Repositories;
using Todos.Infrastructure.Data;

namespace Todos.Infrastructure.Repositories;

public class TodoRepository : Repository<Todo, TodoDbContext>
{
    public TodoRepository(TodoDbContext dbContext) : base(dbContext)
    {
    }
}