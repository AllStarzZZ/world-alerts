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

make the function static and remove the Matches method from #AlertRule

create a repository 'WordEventRepository' which implement only create functionality, use #WorldAlertsDbContext via DI and also create a service collection extension for the repository

Do not use underscore in front of private variables and use primary constructor where it is possible, also update the #design.md file with these code style rules

implement an application service that accepts a #WorldEvent from the api controller through an appropriate DTO class, validates if it's a new event then persists via the repository. Generate the dto, registrate the service into the DI container

implement a controller that accepts a world event like object and stores it via #WorldEventService application service.

modify the api url for #CreateWorldEvent to look like this localhost/api/world-events/add

also update the #design.md to follow this pattern in the rest controllers in the future

I implemented notification channels: #EmailNotificationChannel, #SlackNotificationChannel and a dispatcher #INotificationDispatcher, write the DI registrations into the Infrastructure project

Implement an evaluator service that decides whether the notification dispacher should dispatch to some channel or not. For evaluation use the implemented #EventMatchesAlertRule, for the sake of simplicity do not create a new repository layers (for AlertRules) rather use the #WorldAlertsDbContext as a repository now.

update the #decision-log.md that we don't implement transactions and rollback, because the worst thing that could happen is that we dispatch the notification multiple times after we implement some retry logic upon failure and it is acceptable.

prepare a controller and an endpoint for the admin view according to the sixth point of the mvp in plan.md

modify #GetDashboardSummary and return only the top 20 entities ordered by id desc

extend the repositories Get.*Async methods to accept a count and an order direction then update the #GetDashboardSummary

update the #decision-log.md, we won't create DTO wrapper layer for the admin page since we will have authentication which will protect against schema leaking

update controller setting during startup to identify and ignore cycles during serialization

generate a 'SimpleAuthorizationFilter' attribute to protect #AdminController from unauthorized users that checks if the user provides an identifier in the query strings. The filter should check against a hardcoded value

in #Program.cs implement a light weight guard that checks if the database is exists and creates and applies the migrations if not

take the whole solution and review how well it meets the criteria which is: "We want users to be able to set up alerts so they get notified when something important happens in the world — like breaking news, market movements, natural disasters, that kind of thing. Should work for both email and Slack. Make it flexible enough that we can add more channels later. We need an admin view too."

I think we should provide an endpoint where the user can upload their own #AlertRule. Implement the controller, the application service and the corresponding repository

in #AlertRuleService populate the destination value also since the user should be able to provide that information. Align the DTO class as well

return only the ID in #CreateRuleAsync and do not send the whole object back to the user

#AlertRuleService still returns the whole objec instead of just the id
