var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;



// SERVICES
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

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