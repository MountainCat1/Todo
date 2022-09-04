using Authentication.Service.Abstractions;
using Authentication.Service.Dto;
using Authentication.Service.Errors;
using Authentication.Service.Services;
using Microsoft.AspNetCore.Authentication;

namespace Authentication.Service.Queries.Authenticate;

public class AuthenticateQueryHandler : IQueryHandler<AuthenticateQuery, AuthenticationDto>
{
    private readonly IJWTService _jwtService;
    private readonly IAccountService _accountService;

    public AuthenticateQueryHandler(IAccountService accountService, IJWTService jwtService)
    {
        _accountService = accountService;
        _jwtService = jwtService;
    }

    public async Task<AuthenticationDto> Handle(AuthenticateQuery query, CancellationToken cancellationToken)
    {
        var authenticationResult = await _accountService.AuthenticateAsync(query.Username, query.Password);

        if (!authenticationResult.Succeeded)
            throw new UnauthorizedError();

        var account = authenticationResult.Account;

        var claims = _accountService.GenerateClaimsIdentity(account);

        var authToken = _jwtService.GenerateAsymmetricJwtToken(claims);

        return new AuthenticationDto()
        {
            AuthToken = authToken,
            UserGuid = account.UserGuid
        };
    }
}