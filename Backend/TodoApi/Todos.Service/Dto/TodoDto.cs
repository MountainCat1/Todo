using System.Diagnostics.CodeAnalysis;

namespace Todos.Service.Dto;

public class TodoDto
{
    public Guid Guid { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    
    public ICollection<string> Tags { get; set; }
}