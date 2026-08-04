namespace WorldAlerts.Domain.Enums;

/// <summary>
/// Represents the delivery status of a notification.
/// </summary>
public enum DeliveryStatus
{
    Unknown = 0,
    Pending = 1,
    Sent = 2,
    Failed = 3,
}
