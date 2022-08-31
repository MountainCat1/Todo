using System.ComponentModel.DataAnnotations;
using Todos.Domain.Abstractions;

namespace Todos.Domain.Entities;

public class Todo : IEntity
{
    [Key]
    public Guid Guid { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public Guid TeamGuid { get; set; }
    public Guid AccountGuid { get; set; }

    public ICollection<string> Tags { get; set; }
}