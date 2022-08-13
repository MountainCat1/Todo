using AutoMapper;
using Users.Domain.Entities;
using Users.Domain.Repositories;
using Users.Service.Abstractions;
using Users.Service.Dto;
using Users.Service.Services;

namespace Users.Service.Commands.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationClient _authenticationClient;
    
    public CreateUserCommandHandler(
        IMapper mapper, 
        IUserRepository userRepository, 
        IAuthenticationClient authenticationClient)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _authenticationClient = authenticationClient;
    }

    public async Task<UserDto> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<User>(command.CreateDto);

        var createdEntity = await _userRepository.CreateAsync(entity);

        await _authenticationClient.CreateAccountAsync(new AccountRegisterDto()
        {
            UserGuid = createdEntity.Guid,
            Password = command.CreateDto.Password
        });

        await _userRepository.SaveChangesAsync();

        return _mapper.Map<UserDto>(createdEntity);
    }
}