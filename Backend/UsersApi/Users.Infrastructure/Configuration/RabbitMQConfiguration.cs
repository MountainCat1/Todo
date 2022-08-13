using System.Text.Json.Serialization;

namespace Users.Infrastructure.Configuration;

public class RabbitMQConfiguration
{
    [JsonPropertyName("userName")]
    public string UserName { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("virtualHost")]
    public string VirtualHost { get; set; }

    [JsonPropertyName("hostName")]
    public string HostName { get; set; }
    
    [JsonPropertyName("port")]
    public int Port { get; set; }
}