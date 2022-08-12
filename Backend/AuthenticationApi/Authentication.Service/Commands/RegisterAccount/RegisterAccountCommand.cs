using Authentication.Service.Abstractions;
using Authentication.Service.Dto;

namespace Authentication.Service.Commands.RegisterAccount;

public class RegisterAccountCommand : ICommand<AccountDto>
{
    public AccountRegisterDto RegisterDto { get; set; }
}