using System.Security.Claims;
using AutoMapper;
using Users.Domain.Repositories;
using Users.Service.Abstractions;
using Users.Service.Dto;

namespace Users.Service.Query.GetUserFromClaims;

public class GetUserFromClaimsQueryHandler : IQueryHandler<GetUserFromClaimsQuery, UserDto>
{

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserFromClaimsQueryHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(GetUserFromClaimsQuery query, CancellationToken cancellationToken)
    {
        var accountGuid = query.ClaimsPrincipal.Claims
            .First(claim => claim.Type == ClaimTypes.PrimarySid).Value;

        var entity = await _userRepository.GetOneAsync(user => user.AccountGuid == Guid.Parse(accountGuid));

        var dto = _mapper.Map<UserDto>(entity);

        return dto;
    }
}