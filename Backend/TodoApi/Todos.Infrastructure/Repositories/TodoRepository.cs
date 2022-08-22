using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Todos.Domain.Abstractions;
using Todos.Domain.Entities;
using Todos.Domain.Exceptions;
using Todos.Domain.Repositories;
using Todos.Infrastructure.Data;
using Todos.Infrastructure.Generics;

namespace Todos.Infrastructure.Repositories;

public class TodoRepository : Repository<Todo, TodoDbContext>, ITodoRepository
{
    public TodoRepository(TodoDbContext dbContext) : base(dbContext)
    {
    }
}