using Microsoft.EntityFrameworkCore;
using Teams.Domain.Entities;

namespace Teams.Infrastructure.Data;

public class TeamsDbContext : DbContext
{
    public TeamsDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamMember> TeamMemberships { get; set; }
}