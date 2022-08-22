using Microsoft.EntityFrameworkCore;
using TeamMemberships.Domain.Entities;

namespace TeamMemberships.Infrastructure.Data;

public class TeamMembershipDbContext : DbContext
{
    public TeamMembershipDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeamMembership>()
            .HasKey(entity => new { entity.TeamGuid, UserGuid = entity.AccountGuid });

        // Seeding role table
        modelBuilder.Entity<Role>().HasData(
            new Role { Name = "Admin" }, 
            new Role { Name = "Moderator" }, 
            new Role { Name = "Member" });
    }
    
    public DbSet<TeamMembership> TeamMemberships { get; set; }

    public DbSet<Role> Roles { get; set; }
}