namespace WorldAlerts.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using WorldAlerts.Application.DTOs;
using WorldAlerts.Application.Services;

/// <summary>
/// API controller for managing world events.
/// </summary>
[ApiController]
[Route("api/world-events")]
public class WorldEventsController(
    IWorldEventService worldEventService,
    INotificationEvaluatorService notificationEvaluator) : ControllerBase
{
    /// <summary>
    /// Creates a new world event.
    /// </summary>
    /// <param name="dto">The world event creation data.</param>
    /// <returns>The created world event with assigned ID.</returns>
    /// <response code="201">World event created successfully.</response>
    /// <response code="400">Invalid request data.</response>
    /// <response code="409">A world event with the same external ID already exists.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateWorldEvent([FromBody] CreateWorldEventDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        try
        {
            var createdEvent = await worldEventService.CreateEventAsync(dto);

            await notificationEvaluator.EvaluateAndDispatchAsync(createdEvent, HttpContext.RequestAborted);

            return CreatedAtAction(nameof(CreateWorldEvent), new { id = createdEvent.Id }, createdEvent);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(new { message = $"Invalid input: {ex.ParamName}" });
        }
    }
}
