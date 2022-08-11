var builder = WebApplication.CreateBuilder(args);


// CONFIGURATION
var configuration = builder.Configuration;

configuration.AddEnvironmentVariables();

// SERVICES
var services = builder.Services;


services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

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