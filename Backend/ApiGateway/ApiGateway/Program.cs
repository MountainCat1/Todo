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
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddOcelot(configuration);

// App
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints => {
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.UseOcelot().Wait();


app.Run();