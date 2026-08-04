using WorldAlerts.Domain.Enums;

namespace WorldAlerts.Application.Notification;

public sealed class NotificationDispatcher(
    IEnumerable<INotificationChannel> channels)
    : INotificationDispatcher
{
    public Task<NotificationResult> DispatchAsync(
        NotificationChannelType channelType,
        NotificationMessage message,
        CancellationToken cancellationToken)
    {
        var channel = channels.SingleOrDefault(
            item => item.ChannelType == channelType);

        if (channel is null)
        {
            return Task.FromResult(
                NotificationResult.Failed(
                    $"Notification channel '{channelType}' is not registered."));
        }

        return channel.SendAsync(message, cancellationToken);
    }
}
