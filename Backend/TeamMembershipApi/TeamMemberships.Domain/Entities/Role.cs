using System.ComponentModel.DataAnnotations;
using TeamMemberships.Domain.Abstractions;

namespace TeamMemberships.Domain.Entities;

public class Role : IEntity
{
    [Key]
    public string Name { get; set; }
}