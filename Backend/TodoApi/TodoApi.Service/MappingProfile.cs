using AutoMapper;
using TodoApi.Domain.Entities;
using TodoApi.Service.Dto;

namespace TodoApi.Service;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Todo, TodoDto>();
        CreateMap<TodoDto, Todo>();

        CreateMap<CreateTodoDto, Todo>();
    }
}