using System.Net.Http.Json;
using System.Text.Json;
using Users.Service.Dto;
using Users.Service.Extensions;

namespace Users.Service.Services;

public interface IAuthenticationClient
{
    Task CreateAccountAsync(AccountRegisterDto accountRegisterDto);
    Task<string> AuthenticateAccountAsync(AccountLoginDto accountLoginDto);
}

public class AuthenticationClient : IAuthenticationClient
{
    private readonly HttpClient _httpClient;

    private const string RegisterAccountUri = "Authentication/register";
    private const string AuthenticateAccountUri = "Authentication/authenticate";
    
    public AuthenticationClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateAccountAsync(AccountRegisterDto accountRegisterDto)
    {
        var httpMessage = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            Content = JsonContent.Create(accountRegisterDto),
            RequestUri = new Uri(RegisterAccountUri, UriKind.Relative)
        };
        
        var response = await _httpClient.SendAsync(httpMessage);
    }

    public async Task<string> AuthenticateAccountAsync(AccountLoginDto accountLoginDto)
    {
        var httpMessage = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            Content = JsonContent.Create(accountLoginDto),
            RequestUri = new Uri(AuthenticateAccountUri, UriKind.Relative)
        };

        var response = await _httpClient.SendAsync(httpMessage);

        return await response.Content.GetTextAsync();
    }
}