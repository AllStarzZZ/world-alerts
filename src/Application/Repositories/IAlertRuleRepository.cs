namespace WorldAlerts.Application.Repositories;

using WorldAlerts.Domain.Entities;

/// <summary>
/// Repository interface for managing alert rules.
/// </summary>
public interface IAlertRuleRepository
{
    /// <summary>
    /// Gets all active alert rules with their notification channels.
    /// </summary>
    /// <returns>Collection of active alert rules.</returns>
    Task<IEnumerable<AlertRule>> GetActiveRulesAsync();
}
