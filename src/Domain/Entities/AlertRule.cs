namespace WorldAlerts.Domain.Entities;

using WorldAlerts.Domain.Enums;

/// <summary>
/// Represents the conditions under which a notification should be created.
/// </summary>
public class AlertRule
{
    /// <summary>
    /// Gets or sets the unique identifier for this alert rule.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the alert rule.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the optional event category filter.
    /// If null, this rule applies to all categories.
    /// </summary>
    public EventCategory? Category { get; set; }

    /// <summary>
    /// Gets or sets the minimum severity level required for this rule to match.
    /// </summary>
    public EventSeverity MinimumSeverity { get; set; }

    /// <summary>
    /// Gets or sets the optional keyword filter.
    /// If provided, the event title or description must contain this keyword.
    /// </summary>
    public string? Keyword { get; set; }

    /// <summary>
    /// Gets or sets the optional location filter.
    /// If provided, the event location must match this value.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this alert rule is currently active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation properties
    /// <summary>
    /// Gets or sets the collection of notification channels configured for this rule.
    /// </summary>
    public ICollection<AlertChannel> NotificationChannels { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of notification deliveries triggered by this rule.
    /// </summary>
    public ICollection<NotificationDelivery> NotificationDeliveries { get; set; } = [];
}
