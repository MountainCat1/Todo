using System.Security.Cryptography;
using BunnyOwO;
using BunnyOwO.Configuration;
using BunnyOwO.Extensions;
using BunnyOwO.FluentValidation.Extensions;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Users.Api.Configuration;
using Users.Api.Middleware;
using Users.Domain.Repositories;
using Users.Infrastructure;
using Users.Infrastructure.Data;
using Users.Infrastructure.Events;
using Users.Infrastructure.Repositories;
using Users.Service;
using Users.Service.PipelineBehaviors;
using Users.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURATION
var configuration = builder.Configuration;

configuration.AddEnvironmentVariables();

var jwtConfig = configuration.GetSection(nameof(JWTConfiguration))
    .Get<JWTConfiguration>();

// SERVICES
var services = builder.Services;

services.Configure<RabbitMQConfiguration>(configuration.GetSection(nameof(RabbitMQConfiguration)));

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddHttpsRedirection(options =>
    options.HttpsPort = configuration.GetValue<int>("HTTPS_PORT"));
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

if (builder.Environment.IsDevelopment())
    services.AddDbContext<UserDbContext>(optionsBuilder 
        => optionsBuilder.UseInMemoryDatabase("UserDatabase"));
else
    services.AddDbContext<UserDbContext>(optionsBuilder => 
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"), options =>
        {
            options.EnableRetryOnFailure(maxRetryCount: 3, TimeSpan.FromSeconds(10), null);
        }));

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

services.AddSender();

services.AddAutoMapper(typeof(MappingProfile));
services.AddMediatR(typeof(ServiceAssemblyMarker));
services.AddFluentValidation( new [] { typeof(ServiceAssemblyMarker).Assembly});
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorHandlingBehavior<,>));

services.AddEventHandlersAndReceivers(typeof(ServiceAssemblyMarker));

services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IUserService, UserService>();

services.AddScoped<ErrorHandlingMiddleware>();

// APP
var app = builder.Build();

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

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope
        .ServiceProvider
        .GetRequiredService<UserDbContext>();

    await new DatabaseInitializer(dbContext).InitializeAsync();
}

app.Run();