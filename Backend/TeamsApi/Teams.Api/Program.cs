using System.Security.Cryptography;
using BunnyOwO.Configuration;
using BunnyOwO.Extensions;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Teams.Api.Configuration;
using Teams.Api.Middleware;
using Teams.Domain.Repositories;
using Teams.Infrastructure;
using Teams.Infrastructure.Configuration;
using Teams.Infrastructure.Data;
using Teams.Infrastructure.HttpClients;
using Teams.Infrastructure.Repositories;
using Teams.Service;
using Teams.Service.Command.CreateTodo;
using Teams.Service.PipelineBehaviors;
using Teams.Service.Queries.GetAllTeamTodos;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Configuration
configuration.AddEnvironmentVariables();
configuration.AddJsonFile( 
    builder.Environment.IsDevelopment() 
        ? "microserviceConfiguration.Development.json" 
        : "microserviceConfiguration.json", 
    true);

var jwtConfig = configuration.GetSection(nameof(JWTConfiguration)).Get<JWTConfiguration>();
var microserviceConfiguration = configuration.GetSection(nameof(MicroserviceConfiguration))
        .Get<MicroserviceConfiguration>();

// SERVICES

services.Configure<RabbitMQConfiguration>(configuration.GetSection(nameof(RabbitMQConfiguration)));
services.Configure<MicroserviceConfiguration>(configuration.GetSection(nameof(MicroserviceConfiguration)));

services.AddControllers();
services.AddEndpointsApiExplorer();
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

services.AddHttpsRedirection(options =>
{
    options.HttpsPort = configuration.GetValue<int>("HTTPS_PORT");
});
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var rsa = RSA.Create();
    rsa.ImportRSAPublicKey(
        source: Convert.FromBase64String(jwtConfig.PublicKey),
        bytesRead: out _
    );
    var securityKey = new RsaSecurityKey(rsa);
    
    if(builder.Environment.IsDevelopment())
        options.IncludeErrorDetails = true; // <- great for debugging
    
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig.Issuer,
        ValidAudience = jwtConfig.Audience,
        IssuerSigningKey = securityKey
    };
});

if (builder.Environment.IsDevelopment())
    services.AddDbContext<TeamsDbContext>(optionsBuilder 
        => optionsBuilder.UseInMemoryDatabase("TeamsDatabase"));
else
    services.AddDbContext<TeamsDbContext>(optionsBuilder =>         
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"), options =>
        {
            options.EnableRetryOnFailure(maxRetryCount: 3, TimeSpan.FromSeconds(10), null);
        }));

services.AddSender();
services.AddAutoMapper(typeof(MappingProfile));
services.AddMediatR(typeof(ServiceAssemblyMarker));
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorHandlingBehavior<,>));
services.AddFluentValidation( new [] { typeof(ServiceAssemblyMarker).Assembly});
services.AddEventHandlersAndReceivers(typeof(ServiceAssemblyMarker));

services.AddScoped<ITeamRepository, TeamRepository>();
services.AddHttpClient<ITodoClient, TodoClient>(client 
    => client.BaseAddress = new Uri($"{microserviceConfiguration.Todo}"));
services.AddHttpClient<IMembershipClient, MembershipClient>(client 
    => client.BaseAddress = new Uri($"{microserviceConfiguration.Membership}"));

 
services.AddScoped<ErrorHandlingMiddleware>();

services.AddSingleton<IAuthorizationHandler, GetAllTeamTodosQueryAuthorization>();
services.AddSingleton<IAuthorizationHandler, CreateTodoCommandAuthorization>();

// APP
var app = builder.Build();

if (app.Environment.IsDevelopment() || configuration.GetValue<bool>("ENABLE_SWAGGER"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope
        .ServiceProvider
        .GetRequiredService<TeamsDbContext>();

    await new DatabaseInitializer(dbContext).InitializeAsync();
}


app.Run();