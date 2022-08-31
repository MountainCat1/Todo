using System.Security.Claims;

namespace Teams.Service.Services;

public interface IAccountService
{
    Task<Guid> GetAccountGuidAsync(ClaimsPrincipal claimsPrincipal);
}

public class AccountService : IAccountService
{
    public async Task<Guid> GetAccountGuidAsync(ClaimsPrincipal claimsPrincipal)
    {
        return Guid.Parse(claimsPrincipal.Claims.First(claim => claim.Type == ClaimTypes.PrimarySid).Value);
    }
}