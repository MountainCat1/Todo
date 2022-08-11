using Users.Service.Abstractions;
using Users.Service.Dto;

namespace Users.Service.Query.GetUser;

public class GetUserQuery : IQuery<UserDto>
{
    public GetUserQuery(Guid guid)
    {
        Guid = guid;
    }

    public Guid Guid { get; set; }
}