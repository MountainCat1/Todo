using Microsoft.EntityFrameworkCore;
using TeamMemberships.Domain.Entities;

namespace TeamMemberships.Infrastructure.Data;

public class TeamMembershipDbContext : DbContext
{
    public TeamMembershipDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreatedAsync();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeamMembership>()
            .HasKey(entity => new { entity.TeamGuid, UserGuid = entity.AccountGuid });
    }

    public DbSet<TeamMembership> TeamMemberships { get; set; }
}