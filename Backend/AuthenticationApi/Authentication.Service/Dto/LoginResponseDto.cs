namespace Authentication.Service.Dto;

public class LoginResponseDto
{
    public string AuthToken { get; set; }
    public Guid UserGuid { get; set; }
}