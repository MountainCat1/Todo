using Authentication.Service.Abstractions;
using Authentication.Service.Dto;

namespace Authentication.Service.Queries.Authenticate;

public class AuthenticateQuery : IQuery<AuthenticateResponseDto>
{
    public string Username { get; set; }
    public string Password { get; set; }
}