using System.ComponentModel.DataAnnotations;
using Authentication.Domain.Abstractions;

namespace Authentication.Domain.Entities;

public class Account : IEntity
{
    [Key]
    public Guid Guid { get; set; }
    [Required]
    public Guid UserGuid { get; set; }
    [Required]
    public string PasswordHash { get; set; }
}