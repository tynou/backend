using System.Security.Cryptography;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Notification.Application.Interfaces;

namespace Notification.Infrastructure.Services;

public class EmailProvider : INotificationProvider
{
    public NotificationType Type => NotificationType.Email;
    
    private readonly SmtpOptions _options;

    public EmailProvider(IOptions<SmtpOptions> options)
    {
        _options = options.Value;
    }

    public async Task SendAsync(string recipient, string message)
    {
        Console.WriteLine($"{RandomNumberGenerator.GetInt32(1, 100)} {message}");
        
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("Бэкенд", _options.SenderEmail));
        email.To.Add(new MailboxAddress("Recipient", recipient));
        email.Subject = "Ваш код подтверждения";
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html) 
        { 
            Text = $"<h1>{message}</h1>" 
        };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_options.Host, _options.Port, false);
        await smtp.AuthenticateAsync(_options.Username, _options.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
        
        Console.WriteLine($"Письмо отправлено на {recipient}");
    }
}