using TodoApi.Service.Dto;
using TodoApi.Service.Queries;

namespace TodoApi.Service.Handlers;

public interface IQueryHandler<in T, TResult> where T : IQuery
{
    ValueTask<TResult> Handle(T query, CancellationToken ct);
}