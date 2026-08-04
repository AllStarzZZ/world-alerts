namespace WorldAlerts.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using WorldAlerts.Application.Repositories;
using WorldAlerts.Domain.Entities;
using WorldAlerts.Domain.Enums;
using WorldAlerts.Infrastructure.Data;

/// <summary>
/// Repository for managing world events in the database.
/// </summary>
public class WorldEventRepository(WorldAlertsDbContext dbContext) : IWorldEventRepository
{
    /// <summary>
    /// Creates a new world event in the database.
    /// </summary>
    /// <param name="worldEvent">The world event to create.</param>
    /// <returns>The created world event with the assigned database ID.</returns>
    /// <exception cref="ArgumentNullException">Thrown when worldEvent is null.</exception>
    public async Task<WorldEvent> CreateAsync(WorldEvent worldEvent)
    {
        ArgumentNullException.ThrowIfNull(worldEvent);
        ArgumentNullException.ThrowIfNull(dbContext);

        dbContext.WorldEvents.Add(worldEvent);
        await dbContext.SaveChangesAsync();

        return worldEvent;
    }

    /// <summary>
    /// Checks if a world event with the given external ID already exists.
    /// </summary>
    /// <param name="externalId">The external identifier to check.</param>
    /// <returns>True if an event with the external ID exists, false otherwise.</returns>
    public async Task<bool> ExistsByExternalIdAsync(string externalId)
    {
        ArgumentNullException.ThrowIfNull(externalId);
        ArgumentNullException.ThrowIfNull(dbContext);

        return await dbContext.WorldEvents.AnyAsync(e => e.ExternalId == externalId);
    }

    /// <summary>
    /// Retrieves all world events from the database.
    /// </summary>
    /// <returns>Collection of all world events.</returns>
    public async Task<IEnumerable<WorldEvent>> GetAllAsync()
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        return await dbContext.WorldEvents.ToListAsync();
    }

    /// <summary>
    /// Retrieves world events from the database with optional pagination and sorting.
    /// </summary>
    /// <param name="count">The maximum number of events to return. If null or 0, returns all events.</param>
    /// <param name="sortDirection">The sort direction for the results. Default is Descending.</param>
    /// <returns>Collection of world events ordered by ID.</returns>
    public async Task<IEnumerable<WorldEvent>> GetAllAsync(int? count, SortDirection sortDirection = SortDirection.Descending)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        IQueryable<WorldEvent> query = sortDirection == SortDirection.Descending
            ? dbContext.WorldEvents.OrderByDescending(e => e.Id)
            : dbContext.WorldEvents.OrderBy(e => e.Id);

        if (count.HasValue && count > 0)
        {
            query = query.Take(count.Value);
        }

        return await query.ToListAsync();
    }
}

