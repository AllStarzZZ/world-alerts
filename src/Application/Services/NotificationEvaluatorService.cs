namespace WorldAlerts.Application.Services;

using WorldAlerts.Application.Notification;
using WorldAlerts.Application.Repositories;
using WorldAlerts.Domain.Entities;
using WorldAlerts.Domain.Enums;
using WorldAlerts.Domain.Rules;

/// <summary>
/// Service for evaluating alert rules against world events and dispatching notifications.
/// </summary>
public class NotificationEvaluatorService(
    IAlertRuleRepository alertRuleRepository,
    INotificationDeliveryRepository deliveryRepository,
    INotificationDispatcher dispatcher) : INotificationEvaluatorService
{
    /// <summary>
    /// Evaluates all active alert rules against a world event and dispatches notifications for matching rules.
    /// </summary>
    /// <param name="worldEvent">The world event to evaluate.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of notification deliveries created and persisted.</returns>
    public async Task<IEnumerable<NotificationDelivery>> EvaluateAndDispatchAsync(
        WorldEvent worldEvent,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(worldEvent);
        ArgumentNullException.ThrowIfNull(alertRuleRepository);
        ArgumentNullException.ThrowIfNull(deliveryRepository);
        ArgumentNullException.ThrowIfNull(dispatcher);

        var deliveries = new List<NotificationDelivery>();
        var activeRules = await alertRuleRepository.GetActiveRulesAsync();

        foreach (var rule in activeRules)
        {
            if (!EventMatchesAlertRule.Matches(rule, worldEvent))
            {
                continue;
            }

            foreach (var channel in rule.NotificationChannels)
            {
                var delivery = new NotificationDelivery
                {
                    WorldEventId = worldEvent.Id,
                    AlertRuleId = rule.Id,
                    ChannelType = channel.NotificationChannelType,
                    DeliveryStatus = DeliveryStatus.Pending,
                    CreatedAt = DateTime.UtcNow,
                };

                var message = new NotificationMessage(
                    channel.DestinationValue,
                    $"Alert: {rule.Name}",
                    $"Event: {worldEvent.Title}\nCategory: {worldEvent.Category}\nSeverity: {worldEvent.Severity}");

                try
                {
                    var result = await dispatcher.DispatchAsync(
                        channel.NotificationChannelType,
                        message,
                        cancellationToken);

                    delivery.DeliveryStatus = result.IsSuccessful ? DeliveryStatus.Sent : DeliveryStatus.Failed;
                    delivery.SentAt = result.IsSuccessful ? DateTime.UtcNow : null;
                    delivery.FailureReason = result.IsSuccessful ? null : result.FailureReason;
                }
                catch (Exception ex)
                {
                    delivery.DeliveryStatus = DeliveryStatus.Failed;
                    delivery.FailureReason = $"Dispatcher error: {ex.GetType().Name} - {ex.Message}";
                }

                deliveries.Add(delivery);
            }
        }

        if (deliveries.Count > 0)
        {
            deliveryRepository.AddRange(deliveries);
            await deliveryRepository.SaveChangesAsync(cancellationToken);
        }

        return deliveries;
    }
}

