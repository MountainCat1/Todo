using System.ComponentModel.DataAnnotations;

namespace Authentication.Service.Dto;

public class AccountRegisterDto
{
    [Required]
    public string Password { get; set; }
}
