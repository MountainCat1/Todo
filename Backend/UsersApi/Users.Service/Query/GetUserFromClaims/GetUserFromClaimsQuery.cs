using System.Security.Claims;
using Users.Service.Abstractions;
using Users.Service.Dto;

namespace Users.Service.Query.GetUserFromClaims;

public class GetUserFromClaimsQuery : IQuery<UserDto>
{
    public ClaimsPrincipal ClaimsPrincipal { get; set; }
}