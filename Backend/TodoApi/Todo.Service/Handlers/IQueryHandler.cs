using Todo.Service.Queries;
using Todo.Service.Dto;

namespace Todo.Service.Handlers;

public interface IQueryHandler<in T, TResult> where T : IQuery
{
    ValueTask<TResult> Handle(T query, CancellationToken ct);
}