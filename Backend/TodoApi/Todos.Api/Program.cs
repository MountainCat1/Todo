using MediatR;
using Microsoft.EntityFrameworkCore;
using Todos.Api.Middleware;
using Todos.Infrastructure;
using Todos.Infrastructure.Repositories;
using Todos.Infrastructure.Services;
using Todos.Service;

var builder = WebApplication.CreateBuilder(args);
// Configuration
var configuration = builder.Configuration;


// Add services to the container.
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddHttpsRedirection(options => options.HttpsPort = 5000);
services.AddSwaggerGen();
services.AddLogging(options =>
{
    options.AddConsole();
    options.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
    options.AddFilter("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.Warning);
});

services.AddDbContext<TodoDbContext>(options
    => options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

services.AddAutoMapper(typeof(MappingProfile));

services.AddScoped<ITodoRepository, TodoRepository>();

services.AddMediatR(typeof(Todos.Service.AssemblyPointer).Assembly);

services.AddScoped<ErrorHandlingMiddleware>();

var app = builder.Build();

await new DatabaseInitializer(
        app.Services.CreateAsyncScope()
            .ServiceProvider.GetRequiredService<TodoDbContext>())
    .InitializeAsync(false);

// Configure the HTTP request pipeline.
if (true || app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();