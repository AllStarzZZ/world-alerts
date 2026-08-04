namespace WorldAlerts.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using WorldAlerts.Application.Repositories;
using WorldAlerts.Domain.Entities;
using WorldAlerts.Domain.Enums;
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

    /// <summary>
    /// Retrieves all notification deliveries from the database.
    /// </summary>
    /// <returns>Collection of all notification deliveries.</returns>
    public async Task<IEnumerable<NotificationDelivery>> GetAllAsync()
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        return await dbContext.NotificationDeliveries.ToListAsync();
    }

    /// <summary>
    /// Retrieves notification deliveries from the database with optional pagination and sorting.
    /// </summary>
    /// <param name="count">The maximum number of deliveries to return. If null or 0, returns all deliveries.</param>
    /// <param name="sortDirection">The sort direction for the results. Default is Descending.</param>
    /// <returns>Collection of notification deliveries ordered by ID.</returns>
    public async Task<IEnumerable<NotificationDelivery>> GetAllAsync(int? count, SortDirection sortDirection = SortDirection.Descending)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        IQueryable<NotificationDelivery> query = sortDirection == SortDirection.Descending
            ? dbContext.NotificationDeliveries.OrderByDescending(d => d.Id)
            : dbContext.NotificationDeliveries.OrderBy(d => d.Id);

        if (count.HasValue && count > 0)
        {
            query = query.Take(count.Value);
        }

        return await query.ToListAsync();
    }

    /// <summary>
    /// Retrieves notification deliveries filtered by delivery status.
    /// </summary>
    /// <param name="status">The delivery status to filter by.</param>
    /// <returns>Collection of notification deliveries with the specified status.</returns>
    public async Task<IEnumerable<NotificationDelivery>> GetByStatusAsync(DeliveryStatus status)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        return await dbContext.NotificationDeliveries
            .Where(d => d.DeliveryStatus == status)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves notification deliveries filtered by delivery status with optional pagination and sorting.
    /// </summary>
    /// <param name="status">The delivery status to filter by.</param>
    /// <param name="count">The maximum number of deliveries to return. If null or 0, returns all deliveries.</param>
    /// <param name="sortDirection">The sort direction for the results. Default is Descending.</param>
    /// <returns>Collection of notification deliveries with the specified status, ordered by ID.</returns>
    public async Task<IEnumerable<NotificationDelivery>> GetByStatusAsync(DeliveryStatus status, int? count, SortDirection sortDirection = SortDirection.Descending)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        IQueryable<NotificationDelivery> query = sortDirection == SortDirection.Descending
            ? dbContext.NotificationDeliveries
                .Where(d => d.DeliveryStatus == status)
                .OrderByDescending(d => d.Id)
            : dbContext.NotificationDeliveries
                .Where(d => d.DeliveryStatus == status)
                .OrderBy(d => d.Id);

        if (count.HasValue && count > 0)
        {
            query = query.Take(count.Value);
        }

        return await query.ToListAsync();
    }
}
