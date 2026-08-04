namespace WorldAlerts.Application.Services;

using WorldAlerts.Domain.Entities;

/// <summary>
/// Service interface for evaluating alert rules against world events and dispatching notifications.
/// </summary>
public interface INotificationEvaluatorService
{
    /// <summary>
    /// Evaluates all active alert rules against a world event and dispatches notifications for matching rules.
    /// </summary>
    /// <param name="worldEvent">The world event to evaluate.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task EvaluateAndDispatchAsync(WorldEvent worldEvent, CancellationToken cancellationToken = default);
}
