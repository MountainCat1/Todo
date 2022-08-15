﻿using System.Text.Json.Serialization;

namespace Authentication.Infrastructure.Configuration;

public class RabbitMQConfiguration
{
    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("virtualHost")]
    public string VirtualHost { get; set; }

    [JsonPropertyName("hostName")]
    public string HostName { get; set; }
    
    [JsonPropertyName("port")]
    public int Port { get; set; }
}