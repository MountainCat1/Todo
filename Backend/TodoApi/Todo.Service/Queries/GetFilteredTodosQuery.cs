namespace Todo.Service.Queries;

public class GetFilteredTodosQuery : GetTodosQuery
{
    public GetFilteredTodosQuery(Guid? teamGuid, Guid? userGuid)
    {
        TeamGuid = teamGuid;
        UserGuid = userGuid;
    }

    public Guid? TeamGuid { get; set; }
    public Guid? UserGuid { get; set; }
}