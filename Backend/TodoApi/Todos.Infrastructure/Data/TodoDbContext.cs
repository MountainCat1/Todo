using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Todos.Domain.Entities;

namespace Todos.Infrastructure.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Setting conversion and comparer for tag being collection of strings
        builder.Entity<Todo>().Property(p => p.Tags)
            .HasConversion(
                tagsCollection => string.Join(';', tagsCollection),
                s => s.Split(';', StringSplitOptions.TrimEntries))
            .Metadata.SetValueComparer(new ValueComparer<ICollection<string>>(
                (c1, c2) => c1.SequenceEqual(c2),
                collection => collection.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()));

    }

    public DbSet<Todo> Todos { get; set; }
}