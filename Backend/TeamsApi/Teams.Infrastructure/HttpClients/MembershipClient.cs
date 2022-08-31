using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Runtime.Serialization;
using Teams.Infrastructure.Dto;

namespace Teams.Infrastructure.HttpClients;

public interface IMembershipClient
{
    public Task<MembershipDto?> GetMembershipAsync(Guid teamGuid, Guid accountGuid);
    Task<ICollection<MembershipDto>> GetAccountMembershipsAsync(Guid accountGuid);
}

public class MembershipClient : IMembershipClient
{
    private readonly HttpClient _httpClient;

    private readonly string _getMembershipEndpoint = "get?teamGuid={0}&accountGuid={1}";
    private readonly string _getAccountMembershipsEndpoint = "list?accountGuid={0}";

    public MembershipClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MembershipDto?> GetMembershipAsync(Guid teamGuid, Guid accountGuid)
    {
        var endpoint = string.Format(_getMembershipEndpoint, teamGuid, accountGuid);

        var endpointUri = new Uri(endpoint, UriKind.Relative);

        var response = await _httpClient.GetAsync(endpointUri);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;
        
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(response.ReasonPhrase);

        return await response.Content.ReadFromJsonAsync<MembershipDto?>() 
               ?? throw new SerializationException();
    }

    public async Task<ICollection<MembershipDto>> GetAccountMembershipsAsync(Guid accountGuid)
    {
        var endpoint = string.Format(_getAccountMembershipsEndpoint, accountGuid);

        var endpointUri = new Uri(endpoint, UriKind.Relative);

        var response = await _httpClient.GetAsync(endpointUri);
        
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(response.ReasonPhrase);

        return await response.Content.ReadFromJsonAsync<ICollection<MembershipDto>>() 
               ?? throw new SerializationException();
    }
}


