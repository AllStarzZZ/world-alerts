namespace WorldAlerts.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using WorldAlerts.Application.DTOs;
using WorldAlerts.Application.Services;

/// <summary>
/// API controller for managing alert rules.
/// </summary>
[ApiController]
[Route("api/alert-rules")]
public class AlertRuleController(IAlertRuleService alertRuleService) : ControllerBase
{
    /// <summary>
    /// Creates a new alert rule with configured notification channels.
    /// </summary>
    /// <param name="dto">The alert rule creation data.</param>
    /// <returns>The ID of the created alert rule.</returns>
    /// <response code="201">Alert rule created successfully.</response>
    /// <response code="400">Invalid request data or validation failed.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateAlertRule([FromBody] CreateAlertRuleDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        try
        {
            var ruleId = await alertRuleService.CreateRuleAsync(dto);

            return CreatedAtAction(nameof(CreateAlertRule), new { id = ruleId }, new { id = ruleId });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(new { message = $"Invalid input: {ex.ParamName}" });
        }
    }
}
