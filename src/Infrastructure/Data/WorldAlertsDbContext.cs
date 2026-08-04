namespace WorldAlerts.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using WorldAlerts.Domain.Entities;

/// <summary>
/// The main database context for the World Alerts application.
/// Configured to use SQLite for local persistence.
/// </summary>
public class WorldAlertsDbContext : DbContext
{
    public WorldAlertsDbContext(DbContextOptions<WorldAlertsDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the collection of world events.
    /// </summary>
    public DbSet<WorldEvent> WorldEvents { get; set; }

    /// <summary>
    /// Gets or sets the collection of alert rules.
    /// </summary>
    public DbSet<AlertRule> AlertRules { get; set; }

    /// <summary>
    /// Gets or sets the collection of alert channels.
    /// </summary>
    public DbSet<AlertChannel> AlertChannels { get; set; }

    /// <summary>
    /// Gets or sets the collection of notification deliveries.
    /// </summary>
    public DbSet<NotificationDelivery> NotificationDeliveries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure WorldEvent
        modelBuilder.Entity<WorldEvent>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ExternalId).IsRequired();
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Source).IsRequired();
            entity.Property(e => e.OccurredAt).IsRequired();
            entity.Property(e => e.Category).IsRequired();
            entity.Property(e => e.Severity).IsRequired();
            entity.HasMany(e => e.NotificationDeliveries)
                .WithOne(nd => nd.WorldEvent)
                .HasForeignKey(nd => nd.WorldEventId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure AlertRule
        modelBuilder.Entity<AlertRule>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.MinimumSeverity).IsRequired();
            entity.Property(e => e.IsActive).IsRequired();
            entity.HasMany(e => e.NotificationChannels)
                .WithOne(ac => ac.AlertRule)
                .HasForeignKey(ac => ac.AlertRuleId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(e => e.NotificationDeliveries)
                .WithOne(nd => nd.AlertRule)
                .HasForeignKey(nd => nd.AlertRuleId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure AlertChannel
        modelBuilder.Entity<AlertChannel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AlertRuleId).IsRequired();
            entity.Property(e => e.NotificationChannelType).IsRequired();
            entity.Property(e => e.DestinationValue).IsRequired();
        });

        // Configure NotificationDelivery
        modelBuilder.Entity<NotificationDelivery>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.WorldEventId).IsRequired();
            entity.Property(e => e.AlertRuleId).IsRequired();
            entity.Property(e => e.ChannelType).IsRequired();
            entity.Property(e => e.DeliveryStatus).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
        });
    }
}
