namespace WorldAlerts.Application.Repositories;

using WorldAlerts.Domain.Entities;

/// <summary>
/// Repository interface for managing notification deliveries.
/// </summary>
public interface INotificationDeliveryRepository
{
    /// <summary>
    /// Adds a notification delivery to the repository.
    /// </summary>
    /// <param name="delivery">The notification delivery to add.</param>
    void Add(NotificationDelivery delivery);

    /// <summary>
    /// Adds multiple notification deliveries to the repository.
    /// </summary>
    /// <param name="deliveries">The notification deliveries to add.</param>
    void AddRange(IEnumerable<NotificationDelivery> deliveries);

    /// <summary>
    /// Saves all pending changes to the data store.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of entities saved.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
