using Microsoft.IdentityModel.Tokens;
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
        .AddJsonFile($"ocelotConfiguration.json", true, true)
        .AddJsonFile($"ocelotConfiguration.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
        .AddEnvironmentVariables();
});
host.ConfigureLogging(loggingBuilder => loggingBuilder.AddConsole());

// Services
services.AddCors();

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddOcelot(configuration);

services.AddSwaggerGen();
services.AddSwaggerForOcelot(configuration);

// App
var app = builder.Build();

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints => {
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.UseSwagger();
/*app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger";
});*/
app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
});
    
app.UseOcelot().Wait();


app.Run();