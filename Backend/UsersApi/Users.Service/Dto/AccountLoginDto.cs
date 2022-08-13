namespace Users.Service.Dto;

public class AccountLoginDto
{
    public Guid UserGuid { get; set; }
    public string Password { get; set; }
}