using System.Security.Claims;
using AutoMapper;
using Users.Domain.Repositories;
using Users.Service.Dto;

namespace Users.Service.Services;

public interface IUserService
{
    Task<UserDto> GetUser(ClaimsPrincipal claimsPrincipal);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> GetUser(ClaimsPrincipal claimsPrincipal)
    {
        var accountGuid = claimsPrincipal.Claims
            .First(claim => claim.Type == ClaimTypes.PrimarySid).Value;

        var entity = await _userRepository.GetOneAsync(user => user.AccountGuid == Guid.Parse(accountGuid));

        var dto = _mapper.Map<UserDto>(entity);

        return dto;
    }
}