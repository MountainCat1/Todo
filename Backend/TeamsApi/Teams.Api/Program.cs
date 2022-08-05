using MediatR;
using Microsoft.EntityFrameworkCore;
using Teams.Infrastructure;
using Teams.Infrastructure.Data;
using Teams.Service;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Configuration
configuration.AddEnvironmentVariables();

// SERVICES
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddLogging(options =>
{
    options.AddConsole();
    options.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
    options.AddFilter("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.Warning);
});

services.AddAutoMapper(typeof(MappingProfile));
services.AddMediatR(typeof(ServiceAssemblyPointer));

services.AddDbContext<TeamsDbContext>(options 
    => options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection") 
        ?? throw new ArgumentException("Connection string was not specified")));

// APP
var app = builder.Build();

await new DatabaseInitializer(
        app.Services.CreateAsyncScope()
            .ServiceProvider.GetRequiredService<TeamsDbContext>())
    .InitializeAsync(true);

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