namespace WorldAlerts.Application.Repositories;

using WorldAlerts.Domain.Entities;
using WorldAlerts.Domain.Enums;

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

    /// <summary>
    /// Retrieves all alert rules from the database, including inactive ones.
    /// </summary>
    /// <returns>Collection of all alert rules.</returns>
    Task<IEnumerable<AlertRule>> GetAllAsync();

    /// <summary>
    /// Retrieves alert rules from the database with optional pagination and sorting.
    /// </summary>
    /// <param name="count">The maximum number of rules to return. If null or 0, returns all rules.</param>
    /// <param name="sortDirection">The sort direction for the results. Default is Descending.</param>
    /// <returns>Collection of alert rules ordered by ID.</returns>
    Task<IEnumerable<AlertRule>> GetAllAsync(int? count, SortDirection sortDirection = SortDirection.Descending);

    /// <summary>
    /// Creates a new alert rule in the database.
    /// </summary>
    /// <param name="alertRule">The alert rule to create.</param>
    /// <returns>The created alert rule with the assigned database ID.</returns>
    Task<AlertRule> CreateAsync(AlertRule alertRule);
}
