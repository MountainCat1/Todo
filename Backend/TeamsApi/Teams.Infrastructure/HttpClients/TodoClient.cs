using System.Net.Http.Json;
using System.Runtime.Serialization;
using Teams.Infrastructure.Dto;

namespace Teams.Infrastructure.HttpClients;

public interface ITodoClient
{
    Task<ICollection<TodoDto>> GetTeamTodos(Guid teamGuid);
    Task<ICollection<TodoDto>> GetUserTodos(Guid teamGuid, Guid userGuid);
}

public class TodoClient : ITodoClient
{
    private readonly HttpClient _httpClient;

    private readonly string _getTeamTodosEndpoint = "get?teamGuid={0}";
    private readonly string _getUserTodosEndpoint = "get?teamGuid={0}&accountGuid={1}";
    
    public TodoClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ICollection<TodoDto>> GetTeamTodos(Guid teamGuid)
    {
        var endpoint = string.Format(_getTeamTodosEndpoint, teamGuid);

        var endpointUri = new Uri(endpoint, UriKind.Relative);

        var response = await _httpClient.GetAsync(endpointUri);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(response.ReasonPhrase);

        return await response.Content.ReadFromJsonAsync<ICollection<TodoDto>>() 
               ?? throw new SerializationException();
    }

    public async Task<ICollection<TodoDto>> GetUserTodos(Guid teamGuid, Guid userGuid)
    {
        var endpoint = string.Format(_getUserTodosEndpoint, teamGuid, userGuid);

        var endpointUri = new Uri(endpoint, UriKind.Relative);

        var response = await _httpClient.GetAsync(endpointUri);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(response.ReasonPhrase);

        return await response.Content.ReadFromJsonAsync<ICollection<TodoDto>>() 
               ?? throw new SerializationException();
    }
}