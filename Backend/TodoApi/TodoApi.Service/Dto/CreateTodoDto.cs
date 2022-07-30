namespace TodoApi.Service.Dto;

public class CreateTodoDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public Guid TeamGuid { get; set; }
    public Guid UserGuid { get; set; }
}