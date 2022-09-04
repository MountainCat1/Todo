using AutoMapper;
using Users.Domain.Repositories;
using Users.Service.Abstractions;
using Users.Service.Dto;

namespace Users.Service.Commands.UpdateUser;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    
    public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var updatedEntity = await _userRepository.UpdateAsync(command.UpdateDto, command.Guid);

        await _userRepository.SaveChangesAsync();
        
        return _mapper.Map<UserDto>(updatedEntity);
    }
}