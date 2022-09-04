using Authentication.Service.Abstractions;
using Authentication.Service.Dto;

namespace Authentication.Service.Queries.Authenticate;

public class AuthenticateQuery : IQuery<AuthenticationDto>
{
    public AuthenticateQuery(AccountLoginDto loginDto)
    {
        Username = loginDto.Username;
        Password = loginDto.Password;
    }

    public string Username { get; set; }
    public string Password { get; set; }
}