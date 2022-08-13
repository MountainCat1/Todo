using Authentication.Service.Abstractions;
using Authentication.Service.Dto;

namespace Authentication.Service.Queries.GetAccountJwt;

public class GetAccountJwtQuery : IQuery<string>
{
    public GetAccountJwtQuery(AccountLoginDto loginDto)
    {
        LoginDto = loginDto;
    }

    public AccountLoginDto LoginDto { get; set; }
}