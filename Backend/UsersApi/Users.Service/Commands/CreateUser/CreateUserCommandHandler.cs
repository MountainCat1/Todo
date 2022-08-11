using AutoMapper;
using Users.Domain.Entities;
using Users.Domain.Repositories;
using Users.Service.Abstractions;
using Users.Service.Dto;

namespace Users.Service.Commands.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    
    public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<User>(command.CreateDto);

        var createdEntity = await _userRepository.CreateAsync(entity);

        return _mapper.Map<UserDto>(createdEntity);
    }
}