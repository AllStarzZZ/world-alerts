namespace WorldAlerts.Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorldAlerts.Application.Notification;
using WorldAlerts.Application.Repositories;
using WorldAlerts.Infrastructure.Data;
using WorldAlerts.Infrastructure.Notification;
using WorldAlerts.Infrastructure.Repositories;

/// <summary>
/// Extension methods for registering Infrastructure services into the dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the World Alerts database context and configures SQLite as the data provider.
    /// Also registers repositories, notification channels, and other infrastructure services.
    /// </summary>
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<WorldAlertsDbContext>(options =>
            options.UseSqlite(connectionString));

        // Register repositories
        services.AddScoped<IAlertRuleRepository, AlertRuleRepository>();
        services.AddScoped<INotificationDeliveryRepository, NotificationDeliveryRepository>();

        // Register notification channels
        services.AddScoped<INotificationChannel, EmailNotificationChannel>();
        services.AddScoped<INotificationChannel, SlackNotificationChannel>();

        // Register notification dispatcher
        services.AddScoped<INotificationDispatcher, NotificationDispatcher>();

        return services;
    }
}

