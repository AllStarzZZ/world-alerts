namespace WorldAlerts.Infrastructure.Repositories;

using WorldAlerts.Application.Repositories;
using WorldAlerts.Domain.Entities;
using WorldAlerts.Infrastructure.Data;

/// <summary>
/// EF Core implementation of the notification delivery repository.
/// </summary>
public class NotificationDeliveryRepository(WorldAlertsDbContext dbContext) : INotificationDeliveryRepository
{
    /// <summary>
    /// Adds a notification delivery to the repository.
    /// </summary>
    /// <param name="delivery">The notification delivery to add.</param>
    public void Add(NotificationDelivery delivery)
    {
        dbContext.NotificationDeliveries.Add(delivery);
    }

    /// <summary>
    /// Adds multiple notification deliveries to the repository.
    /// </summary>
    /// <param name="deliveries">The notification deliveries to add.</param>
    public void AddRange(IEnumerable<NotificationDelivery> deliveries)
    {
        dbContext.NotificationDeliveries.AddRange(deliveries);
    }

    /// <summary>
    /// Saves all pending changes to the data store.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of entities saved.</returns>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}
