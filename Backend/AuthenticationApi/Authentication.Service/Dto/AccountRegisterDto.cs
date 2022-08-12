namespace Authentication.Service.Dto;

public class AccountRegisterDto
{
    public Guid UserGuid { get; set; }
    public string Password { get; set; }
}