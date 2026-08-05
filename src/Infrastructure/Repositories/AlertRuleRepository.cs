namespace WorldAlerts.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using WorldAlerts.Application.Repositories;
using WorldAlerts.Domain.Entities;
using WorldAlerts.Domain.Enums;
using WorldAlerts.Infrastructure.Data;

/// <summary>
/// Repository for managing alert rules in the database.
/// </summary>
public class AlertRuleRepository(WorldAlertsDbContext dbContext) : IAlertRuleRepository
{
    /// <summary>
    /// Gets all active alert rules with their notification channels.
    /// </summary>
    /// <returns>Collection of active alert rules.</returns>
    public async Task<IEnumerable<AlertRule>> GetActiveRulesAsync()
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        return await dbContext.AlertRules
            .Where(rule => rule.IsActive)
            .Include(rule => rule.NotificationChannels)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves all alert rules from the database, including inactive ones.
    /// </summary>
    /// <returns>Collection of all alert rules.</returns>
    public async Task<IEnumerable<AlertRule>> GetAllAsync()
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        return await dbContext.AlertRules
            .Include(rule => rule.NotificationChannels)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves alert rules from the database with optional pagination and sorting.
    /// </summary>
    /// <param name="count">The maximum number of rules to return. If null or 0, returns all rules.</param>
    /// <param name="sortDirection">The sort direction for the results. Default is Descending.</param>
    /// <returns>Collection of alert rules ordered by ID.</returns>
    public async Task<IEnumerable<AlertRule>> GetAllAsync(int? count, SortDirection sortDirection = SortDirection.Descending)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        IQueryable<AlertRule> query = sortDirection == SortDirection.Descending
            ? dbContext.AlertRules.OrderByDescending(r => r.Id)
            : dbContext.AlertRules.OrderBy(r => r.Id);

        query = query.Include(rule => rule.NotificationChannels);

        if (count.HasValue && count > 0)
        {
            query = query.Take(count.Value);
        }

        return await query.ToListAsync();
    }

    /// <summary>
    /// Creates a new alert rule in the database.
    /// </summary>
    /// <param name="alertRule">The alert rule to create.</param>
    /// <returns>The created alert rule with the assigned database ID.</returns>
    /// <exception cref="ArgumentNullException">Thrown when alertRule is null.</exception>
    public async Task<AlertRule> CreateAsync(AlertRule alertRule)
    {
        ArgumentNullException.ThrowIfNull(alertRule);
        ArgumentNullException.ThrowIfNull(dbContext);

        dbContext.AlertRules.Add(alertRule);
        await dbContext.SaveChangesAsync();

        return alertRule;
    }
}
