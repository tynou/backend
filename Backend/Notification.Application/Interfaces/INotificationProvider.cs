using Shared.Contracts;

namespace Notification.Application.Interfaces;

public interface INotificationProvider
{
    NotificationType Type { get; } 
    Task SendAsync(string identifier, string message);
}