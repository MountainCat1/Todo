using System.Security.Cryptography;
using System.Text.Json.Serialization;
using Authentication.Api.Extensions;
using Authentication.Api.Middleware;
using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Authentication.Infrastructure.Data;
using Authentication.Infrastructure.Repositories;
using Authentication.Service;
using Authentication.Service.Configuration;
using Authentication.Service.PipelineBehaviors;
using Authentication.Service.Services;
using BunnyOwO.Configuration;
using BunnyOwO.Extensions;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURATION
var configuration = builder.Configuration;

configuration.AddEnvironmentVariables();

var httpsPort = configuration.GetValue<int>("HTTPS_PORT");
var jwtConfig = configuration.GetSection(nameof(JWTConfiguration)).Get<JWTConfiguration>();

// SERVICES
var services = builder.Services;


services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        var origins = configuration.GetValue<string>("CORS_ORIGINS").Split(';');
        policy.WithOrigins(origins);
    });
});

services.Configure<JWTConfiguration>(configuration.GetSection(nameof(JWTConfiguration)));
services.Configure<RabbitMQConfiguration>(configuration.GetSection(nameof(RabbitMQConfiguration)));
services.AddControllers().AddJsonOptions(options => 
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddHttpsRedirection(options => options.HttpsPort = httpsPort);
services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
services.AddLogging(options =>
{
    options.AddConsole();
    options.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
    options.AddFilter("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.Warning);
});

services.AddAsymmetricAuthentication(jwtConfig);

if (builder.Environment.IsDevelopment())
    services.AddDbContext<AccountDbContext>(optionsBuilder 
        => optionsBuilder.UseInMemoryDatabase("AccountDatabase"));
else
    services.AddDbContext<AccountDbContext>(optionsBuilder =>         
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"), options =>
        {
            options.EnableRetryOnFailure(maxRetryCount: 3, TimeSpan.FromSeconds(10), null);
        }));

services.AddSender();

services.AddScoped<IPasswordHasher<Account>, PasswordHasher<Account>>();

services.AddAutoMapper(typeof(MappingProfile));

services.AddScoped<IJWTService, JWTService>();
services.AddScoped<IAccountRepository, AccountRepository>();
services.AddScoped<IAccountService, AccountService>();

services.AddMediatR(typeof(ServiceAssemblyMarker));
services.AddFluentValidation( new [] { typeof(ServiceAssemblyMarker).Assembly});
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorHandlingBehavior<,>));

services.AddEventHandlersAndReceivers(typeof(ServiceAssemblyMarker));

services.AddScoped<ErrorHandlingMiddleware>();

// APP
var app = builder.Build();

app.UseCors();

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment() || configuration.GetValue<bool>("ENABLE_SWAGGER"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if(app.Configuration.GetValue<bool>("ENABLE_HTTPS"))
    app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope
        .ServiceProvider
        .GetRequiredService<AccountDbContext>();

    await new DatabaseInitializer(dbContext).InitializeAsync();
}

app.Run();