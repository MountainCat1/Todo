using System.ComponentModel.DataAnnotations;
using Users.Domain.Abstractions;

namespace Users.Domain.Entities;

public class User : IEntity
{
    [Key]
    public Guid Guid { get; set; }
    
    [MaxLength(32)]
    [MinLength(3)]
    public string Username { get; set; }
}