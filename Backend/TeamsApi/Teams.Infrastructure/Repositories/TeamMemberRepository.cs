using Microsoft.EntityFrameworkCore;
using Teams.Domain.Entities;
using Teams.Domain.Repositories;
using Teams.Infrastructure.Data;
using Teams.Infrastructure.Generics;

namespace Teams.Infrastructure.Repositories;

public class TeamMemberRepository : Repository<TeamMember, TeamsDbContext>, ITeamMemberRepository
{
    public TeamMemberRepository(TeamsDbContext dbContext) : base(dbContext)
    {
    }
}