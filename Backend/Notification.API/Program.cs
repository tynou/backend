using MassTransit;
using Notification.API.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SendExampleConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });
        
        cfg.ReceiveEndpoint("example-service", e =>
        {
            e.ConfigureConsumer<SendExampleConsumer>(context);
        });
    });
});

var app = builder.Build();

app.Run();