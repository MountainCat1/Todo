using Teams.Infrastructure.Abstractions;
using Teams.Infrastructure.Data;
using Teams.Infrastructure.Generics;

namespace Teams.Infrastructure.UnitsOfWork;

public interface ITeamsUnitOfWork : IUnitOfWork<TeamsDbContext>
{
}

public class TeamsUnitOfWork : UnitOfWork<TeamsDbContext>, ITeamsUnitOfWork
{
    public TeamsUnitOfWork(TeamsDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
    {
    }
}