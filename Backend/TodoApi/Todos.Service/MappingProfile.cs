using AutoMapper;
using Todos.Domain.Entities;
using Todos.Service.Dto;

namespace Todos.Service;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Todo, TodoDto>();
        CreateMap<TodoDto, Todo>();

        CreateMap<CreateTodoDto, Todo>();
    }
}