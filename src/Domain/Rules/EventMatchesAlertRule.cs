namespace WorldAlerts.Domain.Rules;

using WorldAlerts.Domain.Entities;
using WorldAlerts.Domain.Enums;

/// <summary>
/// Business rule that determines if a world event matches the conditions of an alert rule.
/// A rule matches when it is active, category and severity filters match (if set), 
/// and at least one of the optional keyword or location filters match (if either is set).
/// Optional filters that are not configured (null) do not restrict matching.
/// </summary>
public class EventMatchesAlertRule
{
    /// <summary>
    /// Determines whether the given alert rule matches the given world event.
    /// </summary>
    /// <param name="alertRule">The alert rule to evaluate.</param>
    /// <param name="worldEvent">The world event to evaluate against the rule.</param>
    /// <returns>True if the rule matches the event, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown when alertRule or worldEvent is null.</exception>
        public static bool Matches(AlertRule alertRule, WorldEvent worldEvent)
    {
        ArgumentNullException.ThrowIfNull(alertRule);
        ArgumentNullException.ThrowIfNull(worldEvent);

        // Rule must be active
        if (!alertRule.IsActive)
        {
            return false;
        }

        // Category filter: if set, event category must match
        if (alertRule.Category.HasValue && worldEvent.Category != alertRule.Category.Value)
        {
            return false;
        }

        // Minimum severity filter: event severity must be >= minimum
        if (worldEvent.Severity < alertRule.MinimumSeverity)
        {
            return false;
        }

        // Optional filters: keyword or location (if either is configured, at least one must match)
        var hasKeywordFilter = !string.IsNullOrEmpty(alertRule.Keyword);
        var hasLocationFilter = !string.IsNullOrEmpty(alertRule.Location);

        if (hasKeywordFilter || hasLocationFilter)
        {
            var keywordMatches = false;
            var locationMatches = false;

            // Check keyword match: must be in title or description (case-insensitive)
            if (hasKeywordFilter)
            {
                var keywordLower = alertRule.Keyword!.ToLowerInvariant();
                var titleMatch = worldEvent.Title.Contains(keywordLower, StringComparison.OrdinalIgnoreCase);
                var descriptionMatch = !string.IsNullOrEmpty(worldEvent.Description) &&
                                       worldEvent.Description.Contains(keywordLower, StringComparison.OrdinalIgnoreCase);
                keywordMatches = titleMatch || descriptionMatch;
            }

            // Check location match: must match event location (case-insensitive)
            if (hasLocationFilter)
            {
                var eventLocationEmpty = string.IsNullOrEmpty(worldEvent.Location);
                locationMatches = !eventLocationEmpty &&
                                  worldEvent.Location!.Equals(alertRule.Location, StringComparison.OrdinalIgnoreCase);
            }

            // If both filters are set, either can match (OR). If only one is set, that one must match.
            var filtersPassed = (hasKeywordFilter && hasLocationFilter)
                ? (keywordMatches || locationMatches)
                : (hasKeywordFilter ? keywordMatches : locationMatches);

            if (!filtersPassed)
            {
                return false;
            }
        }

        return true;
    }
}
