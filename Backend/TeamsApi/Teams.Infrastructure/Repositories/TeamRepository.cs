using Teams.Domain.Entities;
using Teams.Domain.Repositories;
using Teams.Infrastructure.Data;
using Teams.Infrastructure.Generics;

namespace Teams.Infrastructure.Repositories;

public class TeamRepository : Repository<Team, TeamsDbContext>, ITeamRepository
{
    public TeamRepository(TeamsDbContext dbContext) : base(dbContext)
    {
    }
}