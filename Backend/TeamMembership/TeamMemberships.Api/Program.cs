using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamMemberships.Infrastructure.Data;
using TeamMemberships.Service;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURATION
var configuration = builder.Configuration;

configuration.AddEnvironmentVariables();


// SERVICES
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddHttpsRedirection(options =>
    options.HttpsPort = configuration.GetValue<int>("HTTPS_PORT"));

if (builder.Environment.IsDevelopment())
    services.AddDbContext<TeamMembershipDbContext>(optionsBuilder 
        => optionsBuilder.UseInMemoryDatabase("TeamMembershipDatabase"));
else
    services.AddDbContext<TeamMembershipDbContext>(optionsBuilder 
        => optionsBuilder.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

services.AddMediatR(typeof(ServiceAssemplyPointer));


var app = builder.Build();

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