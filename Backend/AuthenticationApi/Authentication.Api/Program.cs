using System.Text;
using Authentication.Api.Configuration;
using Authentication.Api.Middleware;
using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Authentication.Infrastructure.Data;
using Authentication.Infrastructure.Repositories;
using Authentication.Service;
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

JWTConfiguration jwtConfig = new JWTConfiguration();
configuration.Bind("JWTConfiguration", jwtConfig);

// SERVICES
var services = builder.Services;

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