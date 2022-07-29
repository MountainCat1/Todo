namespace TodoApi.Domain.Entities;

public class Todo
{
    public Guid Guid { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
}