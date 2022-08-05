using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Teams.Api.Middleware;
using Teams.Domain.Repositories;
using Teams.Infrastructure;
using Teams.Infrastructure.Data;
using Teams.Infrastructure.Repositories;
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

services.AddDbContext<TeamsDbContext>(options 
    => options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection") 
                            ?? throw new ArgumentException("Connection string was not specified")));

services.AddAutoMapper(typeof(MappingProfile));
services.AddMediatR(typeof(ServiceAssemblyPointer));
// MediaR validation, check it out here: https://www.nuget.org/packages/MediatR.Extensions.FluentValidation.AspNetCore
services.AddFluentValidation( new [] { typeof(ServiceAssemblyPointer).Assembly});

services.AddScoped<ITeamRepository, TeamRepository>();

services.AddScoped<ErrorHandlingMiddleware>();

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

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();