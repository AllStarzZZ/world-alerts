namespace WorldAlerts.Application.Extensions;

using Microsoft.Extensions.DependencyInjection;
using WorldAlerts.Application.Services;

/// <summary>
/// Extension methods for registering Application layer services into the dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the Application layer services.
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IWorldEventService, WorldEventService>();
        services.AddScoped<IAlertRuleService, AlertRuleService>();
        services.AddScoped<INotificationEvaluatorService, NotificationEvaluatorService>();

        return services;
    }
}
