using BunnyOwO;
using BunnyOwO.Configuration;
using BunnyOwO.Extensions;
using BunnyOwO.FluentValidation.Extensions;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Users.Api.Middleware;
using Users.Domain.Repositories;
using Users.Infrastructure;
using Users.Infrastructure.Data;
using Users.Infrastructure.Repositories;
using Users.Service;
using Users.Service.PipelineBehaviors;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURATION
var configuration = builder.Configuration;

configuration.AddEnvironmentVariables();

// SERVICES
var services = builder.Services;

services.Configure<RabbitMQConfiguration>(configuration.GetSection(nameof(RabbitMQConfiguration)));

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddHttpsRedirection(options =>
    options.HttpsPort = configuration.GetValue<int>("HTTPS_PORT"));
services.AddSwaggerGen();
services.AddLogging(options =>
{
    options.AddConsole();
    options.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
    options.AddFilter("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.Warning);
});

if (builder.Environment.IsDevelopment())
    services.AddDbContext<UserDbContext>(optionsBuilder 
        => optionsBuilder.UseInMemoryDatabase("UserDatabase"));
else
    services.AddDbContext<UserDbContext>(optionsBuilder 
        => optionsBuilder.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));
services.AddSender();

services.AddAutoMapper(typeof(MappingProfile));
services.AddMediatR(typeof(ServiceAssemblyMarker));
services.AddFluentValidation( new [] { typeof(ServiceAssemblyMarker).Assembly});
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorHandlingBehavior<,>));

services.AddEventHandlers(typeof(ServiceAssemblyMarker).Assembly);
services.AddEventReceivers(typeof(ServiceAssemblyMarker).Assembly);


services.AddScoped<IUserRepository, UserRepository>();

services.AddScoped<ErrorHandlingMiddleware>();

// APP
var app = builder.Build();

await new DatabaseInitializer(
        app.Services.CreateAsyncScope()
            .ServiceProvider.GetRequiredService<UserDbContext>())
    .InitializeAsync(true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();