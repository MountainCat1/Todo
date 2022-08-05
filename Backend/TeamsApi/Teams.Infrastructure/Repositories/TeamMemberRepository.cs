using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Teams.Domain.Entities;
using Teams.Domain.Repositories;
using Teams.Infrastructure.Data;
using Teams.Infrastructure.Generics;

namespace Teams.Infrastructure.Repositories;

public class TeamMemberRepository : Repository<TeamMember, TeamsDbContext>, ITeamMemberRepository
{
    public TeamMemberRepository(TeamsDbContext dbContext, ILogger<Repository<TeamMember, TeamsDbContext>> logger) : base(dbContext, logger)
    {
    }
}