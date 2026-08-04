namespace WorldAlerts.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using WorldAlerts.Application.Abstractions.Repositories;
using WorldAlerts.Domain.Entities;
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
}

