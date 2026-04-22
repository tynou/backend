using Common.Infrastructure.Extensions;
using Common.Infrastructure.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Notes.Application.Interfaces;
using Notes.Infrastructure.Persistence;
using Notes.Infrastructure.Services;
using Common.Contracts.Grpc;

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

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

builder.Services.AddJwtAuthentication(configuration);

builder.Services.AddAuthorization();

builder.Services.AddControllers();

var connectionString = configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<NoteDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<INoteRepository, NoteRepository>();

builder.Services.AddApplication(configuration["MediatR:Key"], typeof(Notes.Application.AssemblyMarker).Assembly);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddSwaggerGen();

builder.Services.AddGrpcClient<UserVerificationGrpc.UserVerificationGrpcClient>(o => {
    o.Address = new Uri("http://auth-service:7002");
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<NoteDbContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();