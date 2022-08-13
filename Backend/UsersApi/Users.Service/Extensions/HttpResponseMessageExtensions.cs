using System.Text;

namespace Users.Service.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<string> GetTextAsync(this HttpContent httpContent)
    {
        using var reader = new StreamReader(await httpContent.ReadAsStreamAsync(), Encoding.UTF8);
        return await reader.ReadToEndAsync();
    }
}