namespace WorldAlerts.Tests.Domain.Entities;

using WorldAlerts.Domain.Entities;
using WorldAlerts.Domain.Enums;
using WorldAlerts.Domain.Rules;

/// <summary>
/// Tests for the EventMatchesAlertRuleRule business rule.
/// </summary>
public class AlertRuleMatchesTests
{
    /// <summary>
    /// Creates a test world event with default values.
    /// </summary>
    private static WorldEvent CreateTestEvent(
        EventCategory category = EventCategory.Weather,
        EventSeverity severity = EventSeverity.High,
        string? title = null,
        string? description = null,
        string? location = null)
    {
        return new WorldEvent
        {
            Id = 1,
            ExternalId = "test-001",
            Title = title ?? "Test Event Title",
            Description = description,
            Category = category,
            Severity = severity,
            OccurredAt = DateTime.UtcNow,
            Source = "Test Source",
            Location = location,
        };
    }

    /// <summary>
    /// Creates a test alert rule with optional filters.
    /// </summary>
    private static AlertRule CreateTestRule(
        EventCategory? category = null,
        EventSeverity minimumSeverity = EventSeverity.Medium,
        string? keyword = null,
        string? location = null,
        bool isActive = true)
    {
        return new AlertRule
        {
            Id = 1,
            Name = "Test Rule",
            Category = category,
            MinimumSeverity = minimumSeverity,
            Keyword = keyword,
            Location = location,
            IsActive = isActive,
        };
    }

    // Matching tests - all filters configured and passing

    [Test]
    public async Task Matches_WithNoFiltersConfigured_ReturnsTrue()
    {
        var rule = CreateTestRule();
        var worldEvent = CreateTestEvent();

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Matches_WithCategoryFilterMatching_ReturnsTrue()
    {
        var rule = CreateTestRule(category: EventCategory.Weather);
        var worldEvent = CreateTestEvent(category: EventCategory.Weather);

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Matches_WithSeverityEqualToMinimum_ReturnsTrue()
    {
        var rule = CreateTestRule(minimumSeverity: EventSeverity.High);
        var worldEvent = CreateTestEvent(severity: EventSeverity.High);

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Matches_WithSeverityAboveMinimum_ReturnsTrue()
    {
        var rule = CreateTestRule(minimumSeverity: EventSeverity.Medium);
        var worldEvent = CreateTestEvent(severity: EventSeverity.Critical);

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Matches_WithKeywordInTitle_ReturnsTrue()
    {
        var rule = CreateTestRule(keyword: "critical");
        var worldEvent = CreateTestEvent(title: "Critical Weather Event");

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Matches_WithKeywordInDescription_ReturnsTrue()
    {
        var rule = CreateTestRule(keyword: "alert");
        var worldEvent = CreateTestEvent(
            title: "Weather Event",
            description: "This is an alert situation");

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Matches_WithKeywordCaseInsensitiveInTitle_ReturnsTrue()
    {
        var rule = CreateTestRule(keyword: "STORM");
        var worldEvent = CreateTestEvent(title: "Strong storm approaching");

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Matches_WithLocationMatching_ReturnsTrue()
    {
        var rule = CreateTestRule(location: "New York");
        var worldEvent = CreateTestEvent(location: "New York");

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Matches_WithLocationCaseInsensitiveMatching_ReturnsTrue()
    {
        var rule = CreateTestRule(location: "new york");
        var worldEvent = CreateTestEvent(location: "New York");

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Matches_WithAllFiltersConfiguredAndMatching_ReturnsTrue()
    {
        var rule = CreateTestRule(
            category: EventCategory.Weather,
            minimumSeverity: EventSeverity.High,
            keyword: "storm",
            location: "New York");

        var worldEvent = CreateTestEvent(
            category: EventCategory.Weather,
            severity: EventSeverity.Critical,
            title: "Major storm approaching",
            location: "New York");

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    // Non-matching tests - filters configured but not matching

    [Test]
    public async Task Matches_WithInactiveRule_ReturnsFalse()
    {
        var rule = CreateTestRule(isActive: false);
        var worldEvent = CreateTestEvent();

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Matches_WithCategoryFilterNotMatching_ReturnsFalse()
    {
        var rule = CreateTestRule(category: EventCategory.Weather);
        var worldEvent = CreateTestEvent(category: EventCategory.Health);

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Matches_WithSeverityBelowMinimum_ReturnsFalse()
    {
        var rule = CreateTestRule(minimumSeverity: EventSeverity.High);
        var worldEvent = CreateTestEvent(severity: EventSeverity.Medium);

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Matches_WithSeverityLowestBelowMinimum_ReturnsFalse()
    {
        var rule = CreateTestRule(minimumSeverity: EventSeverity.Low);
        var worldEvent = CreateTestEvent(severity: EventSeverity.Unknown);

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Matches_WithKeywordNotInTitleOrDescription_ReturnsFalse()
    {
        var rule = CreateTestRule(keyword: "earthquake");
        var worldEvent = CreateTestEvent(
            title: "Weather Event",
            description: "Heavy rain expected");

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Matches_WithLocationNotMatching_ReturnsFalse()
    {
        var rule = CreateTestRule(location: "Tokyo");
        var worldEvent = CreateTestEvent(location: "New York");

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Matches_WithLocationFilterButEventHasNoLocation_ReturnsFalse()
    {
        var rule = CreateTestRule(location: "New York");
        var worldEvent = CreateTestEvent(location: null);

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Matches_WithOneFilterNotMatching_ReturnsFalse()
    {
        var rule = CreateTestRule(
            category: EventCategory.Weather,
            minimumSeverity: EventSeverity.High,
            keyword: "storm",
            location: "New York");

        // Category doesn't match
        var worldEvent = CreateTestEvent(
            category: EventCategory.Health,
            severity: EventSeverity.Critical,
            title: "Major storm approaching",
            location: "New York");

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsFalse();
    }

    // Optional filter tests - missing filters should not restrict matching

    [Test]
    public async Task Matches_WithNullCategoryFilter_DoesNotRestrictMatching()
    {
        var rule = CreateTestRule(category: null);
        var worldEvent = CreateTestEvent(category: EventCategory.Health);

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Matches_WithNullKeywordFilter_DoesNotRestrictMatching()
    {
        var rule = CreateTestRule(keyword: null);
        var worldEvent = CreateTestEvent(title: "Any event");

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Matches_WithNullLocationFilter_DoesNotRestrictMatching()
    {
        var rule = CreateTestRule(location: null);
        var worldEvent = CreateTestEvent(location: null);

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Matches_WithNullLocationFilterAndEventHasLocation_DoesNotRestrictMatching()
    {
        var rule = CreateTestRule(location: null);
        var worldEvent = CreateTestEvent(location: "New York");

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    // Boundary tests - edge cases

    [Test]
    public async Task Matches_WithEmptyKeywordFilter_DoesNotRestrictMatching()
    {
        var rule = CreateTestRule(keyword: "");
        var worldEvent = CreateTestEvent(title: "Any event");

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Matches_WithEmptyLocationFilter_DoesNotRestrictMatching()
    {
        var rule = CreateTestRule(location: "");
        var worldEvent = CreateTestEvent(location: "New York");

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Matches_WithKeywordWhitespaceOnly_DoesNotMatch()
    {
        var rule = CreateTestRule(keyword: "   ");
        var worldEvent = CreateTestEvent(title: "Event Title");

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Matches_WithLocationWhitespaceOnly_DoesNotMatch()
    {
        var rule = CreateTestRule(location: "   ");
        var worldEvent = CreateTestEvent(location: "New York");

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsFalse();
    }

    // Null parameter tests

    [Test]
    public async Task Matches_WithNullAlertRule_ThrowsArgumentNullException()
    {
        var worldEvent = CreateTestEvent();

        await Assert.That(() => EventMatchesAlertRule.Matches(null!, worldEvent))
            .Throws<ArgumentNullException>();
    }

    [Test]
    public async Task Matches_WithNullWorldEvent_ThrowsArgumentNullException()
    {
        var rule = CreateTestRule();

        await Assert.That(() => EventMatchesAlertRule.Matches(rule, null!))
            .Throws<ArgumentNullException>();
    }

    [Test]
    public async Task Matches_WithEventHasNullDescription_KeywordSearchStillWorks()
    {
        var rule = CreateTestRule(keyword: "storm");
        var worldEvent = CreateTestEvent(
            title: "Major storm approaching",
            description: null);

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Matches_WithEventHasNullDescriptionAndKeywordNotInTitle_ReturnsFalse()
    {
        var rule = CreateTestRule(keyword: "earthquake");
        var worldEvent = CreateTestEvent(
            title: "Weather Event",
            description: null);

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsFalse();
    }

    // Additional test for convenience method delegation

    // Keyword OR Location filter tests - at least one must match when both are configured

    [Test]
    public async Task Matches_WithBothKeywordAndLocationConfigured_KeywordMatches_ReturnsTrue()
    {
        var rule = CreateTestRule(
            keyword: "storm",
            location: "Tokyo");

        var worldEvent = CreateTestEvent(
            title: "Major storm approaching",
            location: "New York");

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Matches_WithBothKeywordAndLocationConfigured_LocationMatches_ReturnsTrue()
    {
        var rule = CreateTestRule(
            keyword: "earthquake",
            location: "New York");

        var worldEvent = CreateTestEvent(
            title: "Weather Event",
            location: "New York");

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Matches_WithBothKeywordAndLocationConfigured_BothMatch_ReturnsTrue()
    {
        var rule = CreateTestRule(
            keyword: "storm",
            location: "New York");

        var worldEvent = CreateTestEvent(
            title: "Major storm approaching",
            location: "New York");

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Matches_WithBothKeywordAndLocationConfigured_NeitherMatches_ReturnsFalse()
    {
        var rule = CreateTestRule(
            keyword: "earthquake",
            location: "Tokyo");

        var worldEvent = CreateTestEvent(
            title: "Weather Event",
            location: "New York");

        var result = EventMatchesAlertRule.Matches(rule, worldEvent);

        await Assert.That(result).IsFalse();
    }
}
