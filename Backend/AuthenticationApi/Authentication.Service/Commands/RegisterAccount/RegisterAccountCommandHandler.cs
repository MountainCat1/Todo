using Authentication.Domain.Repositories;
using Authentication.Service.Abstractions;
using Authentication.Service.Dto;
using Authentication.Service.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Service.Commands.RegisterAccount;

public class RegisterAccountCommandHandler : ICommandHandler<RegisterAccountCommand, AccountDto>
{
    private IMapper _mapper;
    private IAccountService _accountService;
    
    public RegisterAccountCommandHandler(IMapper mapper, IAccountService accountService)
    {
        _mapper = mapper;
        _accountService = accountService;
    }

    public async Task<AccountDto> Handle(RegisterAccountCommand command, CancellationToken cancellationToken)
    {
        var registerDto = command.RegisterDto;

        var createdAccount = await _accountService.RegisterAsync(registerDto.UserGuid, registerDto.Password);

        return _mapper.Map<AccountDto>(createdAccount);
    }
}