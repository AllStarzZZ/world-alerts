namespace WorldAlerts.Application.Abstractions.Repositories;

using WorldAlerts.Domain.Entities;

/// <summary>
/// Repository interface for managing world events.
/// </summary>
public interface IWorldEventRepository
{
    /// <summary>
    /// Creates a new world event in the database.
    /// </summary>
    /// <param name="worldEvent">The world event to create.</param>
    /// <returns>The created world event with the assigned database ID.</returns>
    Task<WorldEvent> CreateAsync(WorldEvent worldEvent);

    /// <summary>
    /// Checks if a world event with the given external ID already exists.
    /// </summary>
    /// <param name="externalId">The external identifier to check.</param>
    /// <returns>True if an event with the external ID exists, false otherwise.</returns>
    Task<bool> ExistsByExternalIdAsync(string externalId);
}
