namespace WorldAlerts.Application.Services;

using WorldAlerts.Application.DTOs;
using WorldAlerts.Domain.Entities;

/// <summary>
/// Service for managing world events.
/// </summary>
public interface IWorldEventService
{
    /// <summary>
    /// Creates a new world event, validating it doesn't already exist by external ID.
    /// </summary>
    /// <param name="dto">The world event creation data.</param>
    /// <returns>The created world event.</returns>
    /// <exception cref="ArgumentNullException">Thrown when dto is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when an event with the same external ID already exists.</exception>
    Task<WorldEvent> CreateEventAsync(CreateWorldEventDto dto);
}
