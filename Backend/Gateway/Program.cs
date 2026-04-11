using Microsoft.OpenApi;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(options =>
// {
//     options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
//     {
//         Type = SecuritySchemeType.Http,
//         Scheme = "bearer",
//         BearerFormat = "JWT",
//         Description = "JWT Authorization header using the Bearer scheme."
//     });
//     options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
//     {
//         [new OpenApiSecuritySchemeReference("bearer", document)] = []
//     });
// });

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerForOcelotUI(options => { options.PathToSwaggerGenerator = "/swagger/docs"; });

app.UseOcelot().Wait();

app.Run();