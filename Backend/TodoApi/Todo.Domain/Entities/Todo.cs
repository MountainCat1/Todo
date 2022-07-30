﻿using System.ComponentModel.DataAnnotations;

namespace Todo.Domain.Entities;

public class Todo
{
    [Key]
    public Guid Guid { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public Guid TeamGuid { get; set; }
    public Guid UserGuid { get; set; }
}