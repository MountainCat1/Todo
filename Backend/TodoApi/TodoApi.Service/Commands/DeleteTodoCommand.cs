﻿namespace TodoApi.Service.Commands;

public class DeleteTodoCommand : ICommand
{
    public Guid Guid { get; init; }
}