namespace WorldAlerts.Application.Services;

using WorldAlerts.Application.DTOs;

/// <summary>
/// Service interface for managing alert rules.
/// </summary>
public interface IAlertRuleService
{
    /// <summary>
    /// Creates a new alert rule with configured notification channels.
    /// </summary>
    /// <param name="dto">The alert rule creation data.</param>
    /// <returns>The ID of the created alert rule.</returns>
    Task<long> CreateRuleAsync(CreateAlertRuleDto dto);
}
