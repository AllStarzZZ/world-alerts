using WorldAlerts.Application.Notification;
using WorldAlerts.Domain.Enums;

namespace WorldAlerts.Infrastructure.Notification;

public sealed class SlackNotificationChannel : INotificationChannel
{
    public NotificationChannelType ChannelType => NotificationChannelType.Slack;

    public async Task<NotificationResult> SendAsync(
        NotificationMessage message,
        CancellationToken cancellationToken)
    {
        try
        {
            if (Random.Shared.Next(101) < 10)
            {
                throw new Exception("Random failure occurred.");
            }

            return NotificationResult.Success();
        }
        catch (Exception exception)
        {
            return NotificationResult.Failed(exception.Message);
        }
    }
}
