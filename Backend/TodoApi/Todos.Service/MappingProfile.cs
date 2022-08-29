using AutoMapper;
using Todos.Domain.Entities;
using Todos.Infrastructure.ExternalMessages;
using Todos.Infrastructure.MessageDto;
using Todos.Service.Commands.CreateTodo;
using Todos.Service.Dto;

namespace Todos.Service;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Todo, TodoDto>();
        CreateMap<TodoDto, Todo>();

        CreateMap<CreateTodoDto, Todo>();

        CreateMap<CreateTodoMessageDto, CreateTodoDto>();
    }
}