using Users.Service.Abstractions;
using Users.Service.Dto;

namespace Users.Service.Query.GetUser;

public class GetUserQuery : IQuery<UserDto>
{
    public Guid Guid { get; set; }
}