namespace WorldAlerts.Application.Services;

using WorldAlerts.Application.Abstractions.Repositories;
using WorldAlerts.Application.DTOs;
using WorldAlerts.Domain.Entities;

/// <summary>
/// Service for managing world events.
/// Handles validation, business logic, and persistence operations.
/// </summary>
public class WorldEventService(IWorldEventRepository repository) : IWorldEventService
{
    /// <summary>
    /// Creates a new world event, validating it doesn't already exist by external ID.
    /// </summary>
    /// <param name="dto">The world event creation data.</param>
    /// <returns>The created world event.</returns>
    /// <exception cref="ArgumentNullException">Thrown when dto is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when an event with the same external ID already exists.</exception>
    public async Task<WorldEvent> CreateEventAsync(CreateWorldEventDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentNullException.ThrowIfNull(repository);

        // Validate that this is a new event (not a duplicate by external ID)
        var eventExists = await repository.ExistsByExternalIdAsync(dto.ExternalId);
        if (eventExists)
        {
            throw new InvalidOperationException(
                $"A world event with external ID '{dto.ExternalId}' already exists.");
        }

        // Map DTO to domain entity
        var worldEvent = new WorldEvent
        {
            ExternalId = dto.ExternalId,
            Title = dto.Title,
            Description = dto.Description,
            Category = dto.Category,
            Severity = dto.Severity,
            OccurredAt = dto.OccurredAt,
            Source = dto.Source,
            Location = dto.Location,
        };

        // Persist to database
        var createdEvent = await repository.CreateAsync(worldEvent);

        return createdEvent;
    }
}
