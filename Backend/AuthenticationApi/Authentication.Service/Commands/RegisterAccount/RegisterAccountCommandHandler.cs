﻿using Authentication.Infrastructure.Events;
using Authentication.Service.Abstractions;
using Authentication.Service.Dto;
using Authentication.Service.Services;
using AutoMapper;
using BunnyOwO;

namespace Authentication.Service.Commands.RegisterAccount;

public class RegisterAccountCommandHandler : ICommandHandler<RegisterAccountCommand, AccountDto>
{
    private readonly IMapper _mapper;
    private readonly IAccountService _accountService;

    private readonly ISender _sender;
    
    public RegisterAccountCommandHandler(IMapper mapper, IAccountService accountService, ISender sender)
    {
        _mapper = mapper;
        _accountService = accountService;
        _sender = sender;
    }

    public async Task<AccountDto> Handle(RegisterAccountCommand command, CancellationToken cancellationToken)
    {
        var registerDto = command.RegisterDto;
        
        var createdAccount = await _accountService.RegisterAsync(registerDto.Username, registerDto.Password);

        var integrationEvent = new AccountCreatedEvent(
            createdAccount.Guid,
            createdAccount.Username, 
            createdAccount.UserGuid);
        
        _sender.PublishEvent("account.event.created", "account.account-created.exchange", integrationEvent);

        return _mapper.Map<AccountDto>(createdAccount);
    }
}