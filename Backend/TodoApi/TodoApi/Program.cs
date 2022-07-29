using Microsoft.EntityFrameworkCore;
using TodoApi.Data.Services;
using TodoApi.Service;
using TodoApi.Service.Queries;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var configuration = builder.Configuration;


// Add services to the container.
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<DatabaseContext>(options 
    => options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

services.AddAutoMapper(typeof(MappingProfile));

services.AddScoped<ITodoQueryHandler, TodoQueryHandler>();

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