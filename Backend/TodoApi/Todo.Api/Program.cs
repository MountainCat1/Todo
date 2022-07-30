using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Data.Repositories;
using Todo.Data.Services;
using Todo.Service;
using Todo.Service.Handlers;
using Todo.Service.Queries;

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

services.AddScoped<ITodoQueryHandler, TodoQueryHandler>();
services.AddScoped<ITodoCommandHandler, TodoCommandHandler>();



var app = builder.Build();

await new DatabaseInitializer(
        app.Services.CreateAsyncScope()
            .ServiceProvider.GetRequiredService<TodoDbContext>())
    .InitializeAsync(app.Environment.IsDevelopment());

// Configure the HTTP request pipeline.
if (true || app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();