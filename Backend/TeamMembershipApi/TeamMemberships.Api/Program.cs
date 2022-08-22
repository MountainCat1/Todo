using BunnyOwO.Configuration;
using BunnyOwO.Extensions;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TeamMemberships.Api.Middleware;
using TeamMemberships.Domain.Repositories;
using TeamMemberships.Infrastructure.Data;
using TeamMemberships.Infrastructure.Repositories;
using TeamMemberships.Service;
using TeamMemberships.Service.PipelineBehaviors;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURATION
var configuration = builder.Configuration;

configuration.AddEnvironmentVariables();


// SERVICES
var services = builder.Services;

services.Configure<RabbitMQConfiguration>(configuration.GetSection(nameof(RabbitMQConfiguration)));

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddLogging(options =>
{
    options.AddConsole();
    options.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
    options.AddFilter("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.Warning);
});

services.AddHttpsRedirection(options =>
    options.HttpsPort = configuration.GetValue<int>("HTTPS_PORT"));

if (builder.Environment.IsDevelopment())
    services.AddDbContext<TeamMembershipDbContext>(optionsBuilder
        => optionsBuilder.UseInMemoryDatabase("TeamMembershipDatabase"));
else
    services.AddDbContext<TeamMembershipDbContext>(optionsBuilder =>         
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"), options =>
        {
            options.EnableRetryOnFailure(maxRetryCount: 3, TimeSpan.FromSeconds(10), null);
        }));

services.AddSender();

services.AddAutoMapper(typeof(MappingProfile));
services.AddMediatR(typeof(ServiceAssemblyPointer));
services.AddFluentValidation(new[] { typeof(ServiceAssemblyPointer).Assembly });
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorHandlingBehavior<,>));
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

services.AddScoped<ITeamMembershipRepository, TeamMembershipRepository>();

services.AddEventHandlersAndReceivers(typeof(ServiceAssemblyPointer));

services.AddScoped<ErrorHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()|| configuration.GetValue<bool>("ENABLE_SWAGGER"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope
        .ServiceProvider
        .GetRequiredService<TeamMembershipDbContext>();

    await new DatabaseInitializer(dbContext).InitializeAsync();
}

app.Run();