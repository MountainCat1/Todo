using System.Net.Http.Json;
using System.Runtime.Serialization;
using Teams.Infrastructure.Dto;

namespace Teams.Infrastructure.HttpClients;

public interface ITodoClient
{
}

public class TodoClient : ITodoClient
{
    private readonly HttpClient _httpClient;

    private readonly string _getTeamTodosEndpoint = "getAll?teamGuid={0}";
    
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

}