namespace WorldAlerts.Application.DTOs;

using System.ComponentModel.DataAnnotations;
using WorldAlerts.Domain.Enums;

/// <summary>
/// Data transfer object for creating a new alert rule.
/// </summary>
public class CreateAlertRuleDto
{
    /// <summary>
    /// Gets or sets the name of the alert rule.
    /// </summary>
    [Required(ErrorMessage = "Alert rule name is required.")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "Alert rule name must be between 1 and 255 characters.")]
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the optional event category filter.
    /// If null, this rule applies to all categories.
    /// </summary>
    public EventCategory? Category { get; set; }

    /// <summary>
    /// Gets or sets the minimum severity level required for this rule to match.
    /// </summary>
    [Required(ErrorMessage = "Minimum severity is required.")]
    public EventSeverity MinimumSeverity { get; set; }

    /// <summary>
    /// Gets or sets the optional keyword filter.
    /// If provided, the event title or description must contain this keyword.
    /// </summary>
    [StringLength(255, ErrorMessage = "Keyword must not exceed 255 characters.")]
    public string? Keyword { get; set; }

    /// <summary>
    /// Gets or sets the optional location filter.
    /// If provided, the event location must match this value.
    /// </summary>
    [StringLength(255, ErrorMessage = "Location must not exceed 255 characters.")]
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this alert rule is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the notification channels to configure for this rule.
    /// Must contain at least one channel with a destination value.
    /// </summary>
    [Required(ErrorMessage = "At least one notification channel must be specified.")]
    [MinLength(1, ErrorMessage = "At least one notification channel must be specified.")]
    public NotificationChannelDto[]? NotificationChannels { get; set; }

    /// <summary>
    /// Data transfer object for notification channel configuration.
    /// </summary>
    public class NotificationChannelDto
    {
        /// <summary>
        /// Gets or sets the notification channel type.
        /// </summary>
        [Required(ErrorMessage = "Notification channel type is required.")]
        public NotificationChannelType ChannelType { get; set; }

        /// <summary>
        /// Gets or sets the destination value for this channel.
        /// </summary>
        [Required(ErrorMessage = "Destination value is required.")]
        [StringLength(512, MinimumLength = 1, ErrorMessage = "Destination value must be between 1 and 512 characters.")]
        public required string DestinationValue { get; set; }
    }
}
