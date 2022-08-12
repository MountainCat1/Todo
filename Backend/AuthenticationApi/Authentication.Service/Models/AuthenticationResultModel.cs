using Authentication.Domain.Entities;

namespace Authentication.Service.Models;

public class AuthenticationResultModel
{
    public enum AuthenticationResult
    {
        Success,
        Failed
    }

    public AuthenticationResult Result { get; set; }
    public Account Account { get; set; }
}