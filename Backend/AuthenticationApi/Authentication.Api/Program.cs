using System.Reflection;
using System.Text;
using Authentication.Api.Middleware;
using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Authentication.Infrastructure;
using Authentication.Infrastructure.Data;
using Authentication.Infrastructure.Repositories;
using Authentication.Service;
using Authentication.Service.Configuration;
using Authentication.Service.PipelineBehaviors;
using Authentication.Service.Services;
using BunnyOwO.Configuration;
using BunnyOwO.Extensions;
using BunnyOwO.FluentValidation.Extensions;
using FluentValidation;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURATION
var configuration = builder.Configuration;

configuration.AddEnvironmentVariables();

var httpsPort = configuration.GetValue<int>("HTTPS_PORT");

// SERVICES
var services = builder.Services;

services.Configure<JWTConfiguration>(configuration.GetSection(nameof(JWTConfiguration)));
services.Configure<RabbitMQConfiguration>(configuration.GetSection(nameof(RabbitMQConfiguration)));

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddHttpsRedirection(options => options.HttpsPort = httpsPort);
services.AddSwaggerGen();
services.AddLogging(options =>
{
    options.AddConsole();
    options.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
    options.AddFilter("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.Warning);
});
if (builder.Environment.IsDevelopment())
    services.AddDbContext<AccountDbContext>(optionsBuilder 
        => optionsBuilder.UseInMemoryDatabase("AccountDatabase"));
else
    services.AddDbContext<AccountDbContext>(optionsBuilder 
        => optionsBuilder.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

services.AddScoped<IPasswordHasher<Account>, PasswordHasher<Account>>();

services.AddBunnyOwOWithValidation(
    typeof(ServiceAssemblyMarker),
    typeof(InfrastructureAssemblyMarker),
    typeof(InfrastructureAssemblyMarker));

/*services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig.Issuer,
        ValidAudience = jwtConfig.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey))
    };
});*/

services.AddAutoMapper(typeof(MappingProfile));

services.AddScoped<IJWTService, JWTService>();
services.AddScoped<IAccountRepository, AccountRepository>();
services.AddScoped<IAccountService, AccountService>();

services.AddMediatR(typeof(ServiceAssemblyMarker));
services.AddFluentValidation( new [] { typeof(ServiceAssemblyMarker).Assembly});
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorHandlingBehavior<,>));

services.AddScoped<ErrorHandlingMiddleware>();

// APP
var app = builder.Build();

await new DatabaseInitializer(
        app.Services.CreateAsyncScope()
            .ServiceProvider.GetRequiredService<AccountDbContext>())
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