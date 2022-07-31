using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Todos.Service.Dto;

public class TodoDto
{
    [Key]
    public Guid Guid { get; set; }
    [MinLength(1)]
    [MaxLength(32)]
    public string Title { get; set; }
    [MaxLength(320)]
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public ICollection<string> Tags { get; set; }
}