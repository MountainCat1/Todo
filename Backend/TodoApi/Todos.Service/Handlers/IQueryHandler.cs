using Todos.Service.Dto;
using Todos.Service.Queries;

namespace Todos.Service.Handlers;

public interface IQueryHandler<in T, TResult> where T : IQuery
{
    ValueTask<TResult> Handle(T query, CancellationToken ct);
}