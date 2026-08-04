namespace WorldAlerts.Domain.Entities;

using WorldAlerts.Domain.Enums;

/// <summary>
/// Represents one notification destination configured for an alert rule.
/// </summary>
public class AlertChannel
{
    /// <summary>
    /// Gets or sets the unique identifier for this alert channel.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the alert rule ID that this channel belongs to.
    /// </summary>
    public long AlertRuleId { get; set; }

    /// <summary>
    /// Gets or sets the type of notification channel.
    /// </summary>
    public NotificationChannelType NotificationChannelType { get; set; }

    /// <summary>
    /// Gets or sets the destination value for this channel.
    /// For Email: email address
    /// For Slack: webhook URL or channel ID
    /// </summary>
    public required string DestinationValue { get; set; }

    // Navigation properties
    /// <summary>
    /// Gets or sets the alert rule that this channel belongs to.
    /// </summary>
    public AlertRule? AlertRule { get; set; }
}
