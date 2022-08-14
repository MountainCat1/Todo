using System.ComponentModel.DataAnnotations;

namespace Authentication.Service.Dto;

public class AccountRegisterDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}
