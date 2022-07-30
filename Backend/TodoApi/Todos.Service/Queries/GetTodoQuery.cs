namespace Todos.Service.Queries;

public class GetTodoQuery : IQuery
{
    public Guid Guid { get; init; }
}