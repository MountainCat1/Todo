namespace Authentication.Service.Dto;

public class AuthenticationDto
{
    public string AuthToken { get; set; }
    public Guid UserGuid { get; set; }
}