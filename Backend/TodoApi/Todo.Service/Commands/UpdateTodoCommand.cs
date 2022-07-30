﻿using Todo.Service.Dto;

namespace Todo.Service.Commands;

public class UpdateTodoCommand : ICommand
{
    public Guid Guid { get; init; }
    public TodoDto TodoDto { get; init; }
}