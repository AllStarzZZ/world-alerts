namespace WorldAlerts.Application.DTOs;

using WorldAlerts.Domain.Enums;

/// <summary>
/// Data Transfer Object for creating a new world event.
/// </summary>
public class CreateWorldEventDto
{
    /// <summary>
    /// Gets or sets the external identifier for this event (from external system).
    /// </summary>
    public required string ExternalId { get; set; }

    /// <summary>
    /// Gets or sets the title of the event.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the optional description of the event.
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
    /// Gets or sets the time when the event occurred.
    /// </summary>
    public DateTime OccurredAt { get; set; }

    /// <summary>
    /// Gets or sets the source of the event.
    /// </summary>
    public required string Source { get; set; }

    /// <summary>
    /// Gets or sets the optional location where the event occurred.
    /// </summary>
    public string? Location { get; set; }
}
