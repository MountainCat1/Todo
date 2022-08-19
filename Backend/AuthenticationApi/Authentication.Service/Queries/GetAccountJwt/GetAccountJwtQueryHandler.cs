using System.Security.Claims;
using Authentication.Service.Abstractions;
using Authentication.Service.Errors;
using Authentication.Service.Services;

namespace Authentication.Service.Queries.GetAccountJwt;

public class GetAccountJwtQueryHandler : IQueryHandler<GetAccountJwtQuery, string>
{
    private readonly IJWTService _jwtService;
    private readonly IAccountService _accountService;

    public GetAccountJwtQueryHandler(IJWTService jwtService, IAccountService accountService)
    {
        _jwtService = jwtService;
        _accountService = accountService;
    }

    public async Task<string> Handle(GetAccountJwtQuery query, CancellationToken cancellationToken)
    {
        var loginDto = query.LoginDto;

        var authenticationResult = await _accountService.AuthenticateAsync(loginDto.Username, loginDto.Password);

        if (!authenticationResult.Succeeded)
            throw new UnauthorizedError();

        var account = authenticationResult.Account;
        
        var jwtToken = _jwtService.GenerateAsymmetricJwtToken(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.UserData, account.UserGuid.ToString()),
                new Claim(ClaimTypes.NameIdentifier, account.Guid.ToString())
            })
        );

        return jwtToken;
    }
}