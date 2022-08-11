using Users.Service.Abstractions;
using Users.Service.Dto;

namespace Users.Service.Query.GetUsers;

public class GetUsersQuery : IQuery<ICollection<UserDto>>
{
}