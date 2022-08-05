using Microsoft.EntityFrameworkCore;
using Teams.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Env Vars
string connectionString = Environment.GetEnvironmentVariable("ConnectionString") ?? throw new ArgumentNullException();

// SERVICES
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<TeamsDbContext>(options => options.UseSqlServer(connectionString));

// APP
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