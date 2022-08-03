using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;
var host = builder.Host;

// Configuration

// Host
host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config
        .AddJsonFile("appsettings.json", true, true)
        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
        .AddJsonFile($"Configuration/ocelotConfiguration.json", true, true)
        .AddJsonFile($"Configuration/ocelotConfiguration.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
        .AddEnvironmentVariables();
});
host.ConfigureLogging(loggingBuilder => loggingBuilder.AddConsole());

// Services
services.AddOcelot(configuration);

// App
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseOcelot().Wait();

app.Run();