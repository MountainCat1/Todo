using Authentication.Domain.Events;
using Authentication.Domain.Repositories;
using Authentication.Infrastructure.Services;
using Authentication.Service.Abstractions;
using Authentication.Service.Dto;
using Authentication.Service.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Service.Commands.RegisterAccount;

public class RegisterAccountCommandHandler : ICommandHandler<RegisterAccountCommand, AccountDto>
{
    private readonly IMapper _mapper;
    private readonly IAccountService _accountService;
    private readonly IRabbitMQClient _rabbitMqClient;
    
    public RegisterAccountCommandHandler(IMapper mapper, IAccountService accountService, IRabbitMQClient rabbitMqClient)
    {
        _mapper = mapper;
        _accountService = accountService;
        _rabbitMqClient = rabbitMqClient;
    }

    public async Task<AccountDto> Handle(RegisterAccountCommand command, CancellationToken cancellationToken)
    {
        var registerDto = command.RegisterDto;

        // TODO: add username for accounts
        var createdAccount = await _accountService.RegisterAsync(Guid.Empty, registerDto.Password);

        var domainEvent = new AccountCreatedDomainEvent(createdAccount.Guid);
        _rabbitMqClient.PublishMessage("account-created-queue", "account.event.created", domainEvent);

        return _mapper.Map<AccountDto>(createdAccount);
    }
}