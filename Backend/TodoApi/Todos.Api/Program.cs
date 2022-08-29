using BunnyOwO.Configuration;
using BunnyOwO.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todos.Api.Middleware;
using Todos.Domain.Repositories;
using Todos.Infrastructure;
using Todos.Infrastructure.Data;
using Todos.Infrastructure.Repositories;
using Todos.Service;
using Todos.Service.PipelineBehaviors;

var builder = WebApplication.CreateBuilder(args);
// Configuration
var configuration = builder.Configuration;

configuration.AddEnvironmentVariables();

// Add services to the container.
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


if (builder.Environment.IsDevelopment())
    services.AddDbContext<TodoDbContext>(options
        => options.UseInMemoryDatabase("TodoDatabase"));   
else
    services.AddDbContext<TodoDbContext>(options
        => options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection") 
            ?? throw new ArgumentException("Connection string was not specified")));

services.AddAutoMapper(typeof(MappingProfile));
services.AddMessageSender();

services.AddScoped<ITodoRepository, TodoRepository>();

services.AddMediatR(typeof(ServiceAssemblyMarker).Assembly);
services.AddValidatorsFromAssembly(typeof(ServiceAssemblyMarker).Assembly);

services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorHandlingBehavior<,>));

services.AddScoped<ErrorHandlingMiddleware>();

services.AddMessageHandlersAndReceivers(typeof(ServiceAssemblyMarker));

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Configuration.GetValue<bool>("ENABLE_SWAGGER"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if(app.Configuration.GetValue<bool>("ENABLE_HTTPS"))
    app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope
        .ServiceProvider
        .GetRequiredService<TodoDbContext>();

    await new DatabaseInitializer(dbContext).InitializeAsync();
}

app.Run();