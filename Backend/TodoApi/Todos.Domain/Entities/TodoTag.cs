using System.ComponentModel.DataAnnotations.Schema;

namespace Todos.Domain.Entities;

public class TodoTag
{
    [ForeignKey(nameof(Todo))]
    public Guid TodoGuid { get; set; }
    public virtual Todo Todo { get; set; }

    public string Name { get; set; }
}