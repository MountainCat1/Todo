using Microsoft.EntityFrameworkCore;

namespace Todo.Infrastructure.Services;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Domain.Entities.Todo> Todos { get; set; }
}