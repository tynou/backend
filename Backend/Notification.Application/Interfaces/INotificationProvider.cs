namespace Notification.Application.Interfaces;

public interface INotificationProvider
{
    NotificationType Type { get; } 
    Task SendAsync(string recipient, string message);
}

public enum NotificationType
{
    Email,
}