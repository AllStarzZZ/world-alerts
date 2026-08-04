namespace WorldAlerts.Application.Notification;

public sealed record NotificationResult(
    bool IsSuccessful,
    string? FailureReason = null)
{
    public static NotificationResult Success()
        => new(true);

    public static NotificationResult Failed(string reason)
        => new(false, reason);
}
