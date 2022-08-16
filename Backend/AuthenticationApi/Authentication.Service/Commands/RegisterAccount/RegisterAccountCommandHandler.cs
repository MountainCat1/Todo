using Authentication.Domain.DomainEvents;
using Authentication.Service.Abstractions;
using Authentication.Service.Dto;
using Authentication.Service.Services;
using AutoMapper;
using BunnyOwO;
using Microsoft.AspNetCore.Identity;

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

        var domainEvent = new AccountCreatedDomainEvent(Guid.Empty);
        _sender.PublishEvent("account.event.created", "account.account-created.exchange", domainEvent);

        return _mapper.Map<AccountDto>(createdAccount);
    }
}