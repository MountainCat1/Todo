﻿namespace Todos.Infrastructure.MessageDto;

public class CreateTodoMessageDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public Guid TeamGuid { get; set; }
    public Guid UserGuid { get; set; }

    public ICollection<string> Tags { get; set; }
}