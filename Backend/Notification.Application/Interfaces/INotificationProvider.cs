namespace Notification.Application.Interfaces;

public interface INotificationProvider
{
    string Type { get; } 
    Task SendAsync(string identifier, string message);
}