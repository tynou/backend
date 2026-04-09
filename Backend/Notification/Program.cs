using MassTransit;
using Notification;

var builder = Host.CreateApplicationBuilder(args);
// builder.Services.AddHostedService<Worker>();

builder.Services.AddMassTransit(x =>
{
    // Регистрируем наш обработчик (Consumer)
    x.AddConsumer<SendExampleConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });

        // "Подписываемся" на очередь
        cfg.ReceiveEndpoint("example-service", e =>
        {
            e.ConfigureConsumer<SendExampleConsumer>(context);
        });
    });
});

var host = builder.Build();
host.Run();