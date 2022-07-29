using System.ComponentModel.DataAnnotations;

namespace TodoApi.Domain.Entities;

public class Todo
{
    [Key]
    public Guid Guid { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
}