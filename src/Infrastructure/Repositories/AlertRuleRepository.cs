namespace WorldAlerts.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using WorldAlerts.Application.Repositories;
using WorldAlerts.Domain.Entities;
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
}
