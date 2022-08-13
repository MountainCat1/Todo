using Authentication.Domain.Entities;

namespace Authentication.Service.Models;


public class AuthenticationResult
{
    public bool Succeeded { get; set; }
    public Account Account { get; set; }
}