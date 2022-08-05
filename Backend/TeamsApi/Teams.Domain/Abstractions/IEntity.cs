using System.ComponentModel.DataAnnotations;

namespace Teams.Domain.Abstractions;

public interface IEntity
{
    [Key]
    public Guid Guid { get; set; }
}