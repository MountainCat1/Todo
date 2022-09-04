namespace Authentication.Service.Dto;

public class AuthenticateResponseDto
{
    public string AuthToken { get; set; }
    public Guid UserGuid { get; set; }
}