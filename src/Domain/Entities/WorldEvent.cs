namespace WorldAlerts.Domain.Entities;

using WorldAlerts.Domain.Enums;

/// <summary>
/// Represents an event received by the platform.
/// </summary>
public class WorldEvent
{
    /// <summary>
    /// Gets or sets the unique identifier for this event.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the external identifier for this event from the source system.
    /// </summary>
    public required string ExternalId { get; set; }

    /// <summary>
    /// Gets or sets the title of the event.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the event.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the category of the event.
    /// </summary>
    public EventCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the severity level of the event.
    /// </summary>
    public EventSeverity Severity { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the event occurred.
    /// </summary>
    public DateTime OccurredAt { get; set; }

    /// <summary>
    /// Gets or sets the source of the event.
    /// </summary>
    public required string Source { get; set; }

    /// <summary>
    /// Gets or sets the optional location information for the event.
    /// </summary>
    public string? Location { get; set; }

    // Navigation properties
    /// <summary>
    /// Gets or sets the collection of notification deliveries for this event.
    /// </summary>
    public ICollection<NotificationDelivery> NotificationDeliveries { get; set; } = [];
}
