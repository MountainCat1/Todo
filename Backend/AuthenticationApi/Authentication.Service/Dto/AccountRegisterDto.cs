using System.ComponentModel.DataAnnotations;

namespace Authentication.Service.Dto;

public class AccountRegisterDto
{
    [Required]
    public Guid UserGuid { get; set; }
    [Required]
    public string Password { get; set; }
}
