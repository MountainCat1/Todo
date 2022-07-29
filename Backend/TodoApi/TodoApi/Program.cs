using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Data.Repositories;
using TodoApi.Data.Services;
using TodoApi.Service;
using TodoApi.Service.Handlers;
using TodoApi.Service.Queries;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var configuration = builder.Configuration;


// Add services to the container.
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddLogging(options => options.AddConsole());

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
    .InitializeAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();