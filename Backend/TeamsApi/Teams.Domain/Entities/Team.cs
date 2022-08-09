using System.ComponentModel.DataAnnotations;
using Teams.Domain.Abstractions;

namespace Teams.Domain.Entities;

public class Team : IEntity
{
    [Key]
    public Guid Guid { get; set; }
    
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid UserGuid { get; set; }
}