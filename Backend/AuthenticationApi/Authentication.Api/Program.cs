using System.Text;
using Authentication.Api.Middleware;
using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Authentication.Infrastructure.Configuration;
using Authentication.Infrastructure.Data;
using Authentication.Infrastructure.Repositories;
using Authentication.Infrastructure.Services;
using Authentication.Service;
using Authentication.Service.Configuration;
using Authentication.Service.PipelineBehaviors;
using Authentication.Service.Services;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURATION
var configuration = builder.Configuration;

configuration.AddEnvironmentVariables();

var httpsPort = configuration.GetValue<int>("HTTPS_PORT");

var jwtConfig = new JWTConfiguration();
configuration.Bind("JWTConfiguration", jwtConfig);

var rabbitMqConfig = new RabbitMQConfiguration();
configuration.Bind("RabbitMQConfiguration", rabbitMqConfig);


// SERVICES
var services = builder.Services;

services.AddSingleton(jwtConfig);
services.AddSingleton(rabbitMqConfig);

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

services.AddSingleton<IRabbitMQClient, RabbitMQClient>();

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
services.AddMediatR(typeof(ServiceAssemblyPointer));
services.AddFluentValidation( new [] { typeof(ServiceAssemblyPointer).Assembly});
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorHandlingBehavior<,>));


services.AddScoped<IJWTService, JWTService>();
services.AddScoped<IAccountRepository, AccountRepository>();
services.AddScoped<IAccountService, AccountService>();

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