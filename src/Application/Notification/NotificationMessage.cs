namespace WorldAlerts.Application.Notification;

public sealed record NotificationMessage(
    string Destination,
    string Subject,
    string Content);
