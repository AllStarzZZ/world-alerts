add a c# style gitignore file to the solution

use #file:'C:\Repos\world-alerts\AGENTS.md' and initialize the project listed under paragraph 'Architecture'

wire the project up into the solution

add a test project that utilizes TUnit, that project should provide tests for all other projects, so modify the .csproj s if necessary and register the must have nuget packages

register the test project into the solution properly

add docs folder that located near to the solution to the solution

Add the following models:
WorldEvent
Represents an event received by the platform.
Include:
Id ExternalId Title Description Category Severity OccurredAt Source optional Location
AlertRule
Represents the conditions under which a notification should be created.
Include:
Id Name optional event category minimum severity optional keyword optional location active status configured notification channels
AlertChannel
Represents one notification destination configured for an alert rule.
Include:
Id NotificationChannelType destination value
Initially support:
Email Slack
NotificationDelivery
Represents one notification attempt created for a matched event and alert rule.
Include:
Id WorldEventId AlertRuleId channel type delivery status creation timestamp optional sent timestamp optional failure reason
Support the following statuses:
Pending Sent Failed
Enums
Create:
EventCategory EventSeverity NotificationChannelType DeliveryStatus

use long for internal ids where it is possible

Implement the Matches(WorldEvent worldEvent) method which is a business rule for   .
A rule matches when it is active and all configured filters match, the logical operator between the filters is 'OR':
category; minimum severity; keyword in title or description; location.
Keyword and location matching must be case-insensitive. Missing optional filters must not restrict matching.
Keep the logic in the Domain layer and add TUnit tests for matching, non-matching, boundary, and null cases.

merge the selected branches, it is enough if the title, description contains the keyword or the location matches

