using MassTransit;
using Notification.API.Consumers;
using Notification.Application.Interfaces;
using Notification.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<INotificationProvider, EmailProvider>();

builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("SmtpOptions"));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SendOtpConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });
        
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.Run();