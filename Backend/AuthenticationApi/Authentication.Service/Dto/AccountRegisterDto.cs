using System.ComponentModel.DataAnnotations;

namespace Authentication.Service.Dto;

public class AccountRegisterDto
{
    [Required]
    public Guid UserGuid { get; set; }
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
