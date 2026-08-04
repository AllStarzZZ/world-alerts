namespace WorldAlerts.Domain.Entities;

using WorldAlerts.Domain.Enums;

/// <summary>
/// Represents one notification attempt created for a matched event and alert rule.
/// </summary>
public class NotificationDelivery
{
    /// <summary>
    /// Gets or sets the unique identifier for this notification delivery.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the world event ID that triggered this notification.
    /// </summary>
    public long WorldEventId { get; set; }

    /// <summary>
    /// Gets or sets the alert rule ID that generated this notification.
    /// </summary>
    public long AlertRuleId { get; set; }

    /// <summary>
    /// Gets or sets the notification channel type used for this delivery.
    /// </summary>
    public NotificationChannelType ChannelType { get; set; }

    /// <summary>
    /// Gets or sets the current delivery status of this notification.
    /// </summary>
    public DeliveryStatus DeliveryStatus { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when this notification delivery was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the optional timestamp when the notification was successfully sent.
    /// </summary>
    public DateTime? SentAt { get; set; }

    /// <summary>
    /// Gets or sets the optional failure reason if the delivery failed.
    /// </summary>
    public string? FailureReason { get; set; }

    // Navigation properties
    /// <summary>
    /// Gets or sets the world event that triggered this delivery.
    /// </summary>
    public WorldEvent? WorldEvent { get; set; }

    /// <summary>
    /// Gets or sets the alert rule that generated this delivery.
    /// </summary>
    public AlertRule? AlertRule { get; set; }
}
