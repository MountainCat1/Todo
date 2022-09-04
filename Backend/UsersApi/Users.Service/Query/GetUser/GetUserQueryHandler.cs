using AutoMapper;
using Users.Domain.Repositories;
using Users.Service.Abstractions;
using Users.Service.Dto;

namespace Users.Service.Query.GetUser;

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;


    public GetUserQueryHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var entity = await _userRepository.GetOneRequiredAsync(query.Guid);

        var dto = _mapper.Map<UserDto>(entity);

        return dto;
    }
}