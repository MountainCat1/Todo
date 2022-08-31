namespace Teams.Service.Dto;

public class UpdateTodoDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public Guid AccountGuid { get; set; }

    public ICollection<string> Tags { get; set; }
}