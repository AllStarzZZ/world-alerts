namespace WorldAlerts.Application.Repositories;

using WorldAlerts.Domain.Entities;
using WorldAlerts.Domain.Enums;

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

    /// <summary>
    /// Retrieves all notification deliveries from the database.
    /// </summary>
    /// <returns>Collection of all notification deliveries.</returns>
    Task<IEnumerable<NotificationDelivery>> GetAllAsync();

    /// <summary>
    /// Retrieves notification deliveries from the database with optional pagination and sorting.
    /// </summary>
    /// <param name="count">The maximum number of deliveries to return. If null or 0, returns all deliveries.</param>
    /// <param name="sortDirection">The sort direction for the results. Default is Descending.</param>
    /// <returns>Collection of notification deliveries ordered by ID.</returns>
    Task<IEnumerable<NotificationDelivery>> GetAllAsync(int? count, SortDirection sortDirection = SortDirection.Descending);

    /// <summary>
    /// Retrieves notification deliveries filtered by delivery status.
    /// </summary>
    /// <param name="status">The delivery status to filter by.</param>
    /// <returns>Collection of notification deliveries with the specified status.</returns>
    Task<IEnumerable<NotificationDelivery>> GetByStatusAsync(DeliveryStatus status);

    /// <summary>
    /// Retrieves notification deliveries filtered by delivery status with optional pagination and sorting.
    /// </summary>
    /// <param name="status">The delivery status to filter by.</param>
    /// <param name="count">The maximum number of deliveries to return. If null or 0, returns all deliveries.</param>
    /// <param name="sortDirection">The sort direction for the results. Default is Descending.</param>
    /// <returns>Collection of notification deliveries with the specified status, ordered by ID.</returns>
    Task<IEnumerable<NotificationDelivery>> GetByStatusAsync(DeliveryStatus status, int? count, SortDirection sortDirection = SortDirection.Descending);
}
