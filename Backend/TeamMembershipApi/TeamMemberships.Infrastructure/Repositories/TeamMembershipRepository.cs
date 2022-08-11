using TeamMemberships.Domain.Entities;
using TeamMemberships.Domain.Repositories;
using TeamMemberships.Infrastructure.Data;
using TeamMemberships.Infrastructure.Generics;

namespace TeamMemberships.Infrastructure.Repositories;

public class TeamMembershipRepository : Repository<TeamMembership, TeamMembershipDbContext>, ITeamMembershipRepository
{
    public TeamMembershipRepository(TeamMembershipDbContext dbContext) : base(dbContext)
    {
    }
}