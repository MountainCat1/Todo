using AutoMapper;
using Users.Domain.Repositories;
using Users.Service.Abstractions;
using Users.Service.Dto;

namespace Users.Service.Query.GetUsers;

public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, ICollection<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<ICollection<UserDto>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var entities = await _userRepository.GetAllAsync();

        var dto = _mapper.Map<ICollection<UserDto>>(entities);

        return dto;
    }
}