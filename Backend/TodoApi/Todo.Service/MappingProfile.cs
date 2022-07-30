using AutoMapper;
using Todo.Service.Dto;
using Todo.Domain.Entities;

namespace Todo.Service;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Todo, TodoDto>();
        CreateMap<TodoDto, Domain.Entities.Todo>();

        CreateMap<CreateTodoDto, Domain.Entities.Todo>();
    }
}