using Microsoft.EntityFrameworkCore;
using Teams.Domain.Entities;
using Teams.Domain.Repositories;
using Teams.Infrastructure.Data;
using Teams.Infrastructure.Exceptions;
using Teams.Infrastructure.Generics;

namespace Teams.Infrastructure.Repositories;

public class TeamRepository : Repository<Team>, ITeamRepository
{
    public TeamRepository(DbContext dbContext) : base(dbContext)
    {
    }
}