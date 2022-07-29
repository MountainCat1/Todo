using TodoApi.Service.Dto;

namespace TodoApi.Service.Queries;

public interface IQueryHandler<in T, TResult> where T : IQuery
{
    ValueTask<ICollection<TodoDto>> Handle(T query, CancellationToken ct);
}