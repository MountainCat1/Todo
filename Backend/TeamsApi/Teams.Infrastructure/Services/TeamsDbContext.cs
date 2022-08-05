using Microsoft.EntityFrameworkCore;
using Teams.Domain.Entities;

namespace Teams.Infrastructure.Services;

public class TeamsDbContext : DbContext
{
    public TeamsDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Team> Type { get; set; }
}