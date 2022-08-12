using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Authentication.Infrastructure.Data;
using Authentication.Infrastructure.Generics;

namespace Authentication.Infrastructure.Repositories;

public class AccountRepository : Repository<Account, AccountDbContext>, IAccountRepository
{
    public AccountRepository(AccountDbContext dbContext) : base(dbContext)
    {
    }
}