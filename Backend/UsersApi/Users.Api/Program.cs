using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Users.Api.Middleware;
using Users.Domain.Repositories;
using Users.Infrastructure.Configuration;
using Users.Infrastructure.Data;
using Users.Infrastructure.RabbitMQ;
using Users.Infrastructure.RabbitMQ.Events;
using Users.Infrastructure.RabbitMQ.Extensions;
using Users.Infrastructure.Repositories;
using Users.Service;
using Users.Service.PipelineBehaviors;
using ISender = Users.Infrastructure.RabbitMQ.ISender;

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

services.AddSingleton<ISender, Sender>();
services.AddEventHandlers(typeof(IEvent).Assembly);
services.AddEventReceivers(typeof(IEvent).Assembly);

services.AddAutoMapper(typeof(MappingProfile));
services.AddMediatR(typeof(ServiceAssemblyPointer));
services.AddFluentValidation( new [] { typeof(ServiceAssemblyPointer).Assembly});
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorHandlingBehavior<,>));

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