namespace WorldAlerts.Application.Services;

using WorldAlerts.Application.DTOs;
using WorldAlerts.Application.Repositories;
using WorldAlerts.Domain.Entities;

/// <summary>
/// Service for managing alert rules.
/// Handles validation, business logic, and persistence operations.
/// </summary>
public class AlertRuleService(IAlertRuleRepository repository) : IAlertRuleService
{
    /// <summary>
    /// Creates a new alert rule with configured notification channels.
    /// </summary>
    /// <param name="dto">The alert rule creation data.</param>
    /// <returns>The ID of the created alert rule.</returns>
    /// <exception cref="ArgumentNullException">Thrown when dto or notification channels are null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when notification channels are empty.</exception>
    public async Task<long> CreateRuleAsync(CreateAlertRuleDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentNullException.ThrowIfNull(dto.NotificationChannels);

        if (dto.NotificationChannels.Length == 0)
        {
            throw new InvalidOperationException("At least one notification channel must be specified.");
        }

        foreach (var channel in dto.NotificationChannels)
        {
            ArgumentNullException.ThrowIfNull(channel);

            if (!Enum.IsDefined(channel.ChannelType))
            {
                throw new InvalidOperationException(
                    $"Invalid notification channel type: {channel.ChannelType}.");
            }
        }

        var alertRule = new AlertRule
        {
            Name = dto.Name,
            Category = dto.Category,
            MinimumSeverity = dto.MinimumSeverity,
            Keyword = dto.Keyword,
            Location = dto.Location,
            IsActive = dto.IsActive,
            NotificationChannels = [.. dto.NotificationChannels
                .Select(channel => new AlertChannel
                {
                    NotificationChannelType = channel.ChannelType,
                    DestinationValue = channel.DestinationValue,
                })],
        };

        var createdRule = await repository.CreateAsync(alertRule);

        return createdRule.Id;
    }
}
