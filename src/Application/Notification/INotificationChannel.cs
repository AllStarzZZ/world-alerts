namespace WorldAlerts.Application.Notification;

using WorldAlerts.Domain.Enums;

public interface INotificationChannel
{
    NotificationChannelType ChannelType { get; }

    Task<NotificationResult> SendAsync(NotificationMessage message, CancellationToken cancellationToken);
}