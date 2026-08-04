namespace WorldAlerts.Application.Repositories;

using WorldAlerts.Domain.Entities;
using WorldAlerts.Domain.Enums;

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

    /// <summary>
    /// Retrieves all world events from the database.
    /// </summary>
    /// <returns>Collection of all world events.</returns>
    Task<IEnumerable<WorldEvent>> GetAllAsync();

    /// <summary>
    /// Retrieves world events from the database with optional pagination and sorting.
    /// </summary>
    /// <param name="count">The maximum number of events to return. If null or 0, returns all events.</param>
    /// <param name="sortDirection">The sort direction for the results. Default is Descending.</param>
    /// <returns>Collection of world events ordered by ID.</returns>
    Task<IEnumerable<WorldEvent>> GetAllAsync(int? count, SortDirection sortDirection = SortDirection.Descending);
}
