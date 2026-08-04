using WorldAlerts.Domain.Enums;

namespace WorldAlerts.Application.Notification;

public interface INotificationDispatcher
{
    Task<NotificationResult> DispatchAsync(
        NotificationChannelType channelType,
        NotificationMessage message,
        CancellationToken cancellationToken);
}
