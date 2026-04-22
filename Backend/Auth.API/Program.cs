using Auth.API.GrpcServices;
using Auth.Application.Interfaces;
using Auth.Infrastructure.Auth;
using Auth.Infrastructure.Persistence;
using Auth.Infrastructure.Services;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.Middleware;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("bearer", document)] = []
    });
});

builder.Services.AddControllers();

var connectionString = configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AuthDbContext>(options => options.UseNpgsql(connectionString));

var redisHost = configuration["Redis:Host"] ?? "localhost";
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisHost));
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddApplication(configuration["MediatR:Key"], typeof(Auth.Application.AssemblyMarker).Assembly);

builder.Services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddSwaggerGen();

builder.Services.AddGrpc();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitHost = configuration["RabbitMQ:Host"] ?? "localhost";
        cfg.Host(rabbitHost, "/", h => {
            h.Username("guest");
            h.Password("guest");
        });
    });
});

builder.Services.AddJwtAuthentication(configuration);

builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapGrpcService<UserVerificationGrpcService>();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();