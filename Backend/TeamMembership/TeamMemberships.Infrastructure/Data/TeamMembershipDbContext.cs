using Microsoft.EntityFrameworkCore;
using TeamMemberships.Domain.Entities;

namespace TeamMemberships.Infrastructure.Data;

public class TeamMembershipDbContext : DbContext
{
    public TeamMembershipDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<TeamMembership> TeamMemberships { get; set; }
}