namespace WorldAlerts.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using WorldAlerts.Api.Filters;
using WorldAlerts.Application.Repositories;
using WorldAlerts.Domain.Enums;

/// <summary>
/// API controller for admin dashboard and monitoring endpoints.
/// Exposes world events, alert rules, and notification delivery statuses.
/// Protected by SimpleAuthorizationFilter which requires a valid admin key in the query string.
/// </summary>
[ApiController]
[Route("api/admin")]
[SimpleAuthorizationFilter]
public class AdminController(
    IWorldEventRepository worldEventRepository,
    IAlertRuleRepository alertRuleRepository,
    INotificationDeliveryRepository deliveryRepository) : ControllerBase
{
    /// <summary>
    /// Retrieves all world events.
    /// </summary>
    /// <returns>Collection of all world events in the system.</returns>
    /// <response code="200">World events retrieved successfully.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("events")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetAllEvents()
    {
        try
        {
            var events = await worldEventRepository.GetAllAsync();
            return Ok(new { count = events.Count(), events });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "An error occurred while retrieving events.", error = ex.Message });
        }
    }

    /// <summary>
    /// Retrieves all alert rules (including inactive ones).
    /// </summary>
    /// <returns>Collection of all alert rules in the system.</returns>
    /// <response code="200">Alert rules retrieved successfully.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("alert-rules")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetAllAlertRules()
    {
        try
        {
            var rules = await alertRuleRepository.GetAllAsync();
            return Ok(new { count = rules.Count(), rules });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "An error occurred while retrieving alert rules.", error = ex.Message });
        }
    }

    /// <summary>
    /// Retrieves all notification deliveries.
    /// </summary>
    /// <returns>Collection of all notification delivery records.</returns>
    /// <response code="200">Notification deliveries retrieved successfully.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("deliveries")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetAllDeliveries()
    {
        try
        {
            var deliveries = await deliveryRepository.GetAllAsync();
            return Ok(new { count = deliveries.Count(), deliveries });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "An error occurred while retrieving notification deliveries.", error = ex.Message });
        }
    }

    /// <summary>
    /// Retrieves notification deliveries filtered by delivery status.
    /// </summary>
    /// <param name="status">The delivery status to filter by (Pending, Sent, or Failed).</param>
    /// <returns>Collection of notification deliveries with the specified status.</returns>
    /// <response code="200">Notification deliveries retrieved successfully.</response>
    /// <response code="400">Invalid status value provided.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("deliveries/status/{status}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetDeliveriesByStatus(DeliveryStatus status)
    {
        try
        {
            var deliveries = await deliveryRepository.GetByStatusAsync(status);
            return Ok(new { status = status.ToString(), count = deliveries.Count(), deliveries });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = "Invalid delivery status provided.", error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "An error occurred while retrieving notification deliveries.", error = ex.Message });
        }
    }

    /// <summary>
    /// Retrieves dashboard summary statistics with top 20 entities ordered by ID descending.
    /// </summary>
    /// <returns>Summary statistics about events, rules, and delivery statuses (limited to top 20 entities each).</returns>
    /// <response code="200">Dashboard statistics retrieved successfully.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("dashboard/summary")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetDashboardSummary()
    {
        try
        {
            var events = await worldEventRepository.GetAllAsync(20, SortDirection.Descending);
            var rules = await alertRuleRepository.GetAllAsync(20, SortDirection.Descending);
            var deliveries = await deliveryRepository.GetAllAsync(20, SortDirection.Descending);
            var successfulDeliveries = await deliveryRepository.GetByStatusAsync(DeliveryStatus.Sent, 20, SortDirection.Descending);
            var failedDeliveries = await deliveryRepository.GetByStatusAsync(DeliveryStatus.Failed, 20, SortDirection.Descending);
            var pendingDeliveries = await deliveryRepository.GetByStatusAsync(DeliveryStatus.Pending, 20, SortDirection.Descending);

            var summary = new
            {
                totalEvents = events.Count(),
                totalAlertRules = rules.Count(),
                activeAlertRules = rules.Count(r => r.IsActive),
                totalDeliveries = deliveries.Count(),
                successfulDeliveries = successfulDeliveries.Count(),
                failedDeliveries = failedDeliveries.Count(),
                pendingDeliveries = pendingDeliveries.Count(),
                recentEvents = events,
                recentRules = rules,
                recentDeliveries = deliveries,
            };

            return Ok(summary);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "An error occurred while retrieving dashboard summary.", error = ex.Message });
        }
    }
}
