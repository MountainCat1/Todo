using Users.Domain.Entities;
using Users.Domain.Repositories;
using Users.Infrastructure.Data;
using Users.Infrastructure.Generics;

namespace Users.Infrastructure.Repositories;

public class UserRepository : Repository<User, UserDbContext>, IUserRepository
{
    public UserRepository(UserDbContext dbContext) : base(dbContext)
    {
    }
}