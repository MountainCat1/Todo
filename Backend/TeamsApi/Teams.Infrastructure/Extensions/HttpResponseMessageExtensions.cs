using System.Net.Http.Json;

namespace Teams.Infrastructure.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<T> GetObject<T>(this HttpResponseMessage httpResponseMessage)
    {
        return await httpResponseMessage.Content.ReadFromJsonAsync<T>();
    }
}