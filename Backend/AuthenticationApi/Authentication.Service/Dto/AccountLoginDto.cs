using System.ComponentModel.DataAnnotations;

namespace Authentication.Service.Dto;

public class AccountLoginDto
{
    [Required]
    public Guid UserGuid { get; set; }
    [Required]
    public string Password { get; set; }
}